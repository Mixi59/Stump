﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using Stump.Core.Attributes;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.AuthServer.Database;
using Stump.Server.AuthServer.IPC;
using Stump.Server.AuthServer.Managers;
using Stump.Server.AuthServer.Network;
using Stump.Server.BaseServer;
using Stump.Server.BaseServer.Database;
using Stump.Server.BaseServer.Network;
using Stump.Server.BaseServer.Plugins;

namespace Stump.Server.AuthServer
{
    public class AuthServer : ServerBase<AuthServer>
    {
        [Variable]
        public static DatabaseConfiguration AuthDatabaseConfiguration = new DatabaseConfiguration
        {
            DatabaseType = Castle.ActiveRecord.Framework.Config.DatabaseType.MySql,
            Host = "localhost",
            Name = "stump_auth",
            User = "root",
            Password = "",
            UpdateFileDir = "./sql_update/auth/",
        };

        public AuthServer() :
            base(Definitions.ConfigFilePath, Definitions.SchemaFilePath)
        {
        }

        public override void Initialize()
        {
            try
            {
                base.Initialize();
                ConsoleInterface = new AuthConsole();
                ConsoleBase.SetTitle("#Stump Authentification Server");


                logger.Info("Initializing Database...");
                DatabaseAccessor = new DatabaseAccessor(AuthDatabaseConfiguration, Definitions.DatabaseRevision, typeof(AuthBaseRecord<>), Assembly.GetExecutingAssembly());
                DatabaseAccessor.Initialize();

                logger.Info("Opening Database...");
                DatabaseAccessor.OpenDatabase();

                logger.Info("Register Messages...");
                MessageReceiver.Initialize();
                ProtocolTypeManager.Initialize();

                logger.Info("Register Packets Handlers...");
                HandlerManager.RegisterAll(Assembly.GetExecutingAssembly());

                logger.Info("Register Commands...");
                CommandManager.RegisterAll(Assembly.GetExecutingAssembly());

                logger.Info("Starting Console Handler Interface...");
                ConsoleInterface.Start();

                logger.Info("Start World Servers Manager");
                WorldServerManager.Instance.Initialize();
                WorldServerManager.Instance.Start();

                logger.Info("Starting IPC Server..");
                IpcServer.Instance.Initialize();
            }
            catch (Exception ex)
            {
                HandleCrashException(ex);
                Shutdown();
            }
        }

        protected override void OnPluginAdded(PluginContext plugincontext)
        {
            CommandManager.RegisterAll(plugincontext.PluginAssembly);

            base.OnPluginAdded(plugincontext);
        }

        public override void OnShutdown()
        {
        }

        protected override BaseClient CreateClient(Socket s)
        {
            return new AuthClient(s);
        }

        public IEnumerable<AuthClient> FindClients(Predicate<AuthClient> predicate)
        {
            return (from AuthClient entry in MessageListener.ClientList
                    where predicate(entry)
                    select entry);
        }
    }
}