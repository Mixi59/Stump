﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ProtoBuf;
using Stump.BaseCore.Framework.Attributes;
using Stump.Server.DataProvider.Core2;

namespace Stump.Server.DataProvider.Core
{
    static class DataLoader
    {
        /// <summary>
        ///  Contains all of params of the providers
        /// </summary>
        [Variable]
        public static List<ProviderParams> ProviderParams = new List<ProviderParams>();

        public static void Init()
        {
            var @params = ProviderParams.ToDictionary(p => p.ProviderType);

            var asm = Assembly.GetExecutingAssembly();
            var attribType = typeof(DataProviderAttribute);

            foreach (var type in asm.GetTypes())
            {
                var attrib = type.GetCustomAttributes(attribType, false).FirstOrDefault() as DataProviderAttribute;

                if (attrib != null)
                {
                    var field = type.GetField("Instance");
                    var method = type.GetMethod("Init");
                    if (field != null && method != null)
                    {
                        var instance = field.GetValue(null);

                        if (@params.ContainsKey(type))
                            type.GetMethod("Init").Invoke(instance, new object[] { @params[type] });
                        else
                            type.GetMethod("Init").Invoke(instance, new object[] { new ProviderParams { LoadingType = LoadingType.PreLoading, LifeTime = 60000, CheckTime = 60000 } });
                    }
                }
            }
        }

    }
}