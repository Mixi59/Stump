using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Stump.Core.IO;
using Stump.DofusProtocol.Enums;

namespace Stump.Server.BaseServer.Commands
{
    public abstract class TriggerBase
    {
        private readonly Regex m_regexIsNamed = new Regex(@"^(?!\"")(?:-|--)?(\w+)=([^\""\s]*)(?!\"")$", RegexOptions.Compiled);
        private readonly Regex m_regexVar = new Regex(@"^(?!\"")(?:-|--)([a-zA-Z]+)(?!\"")$", RegexOptions.Compiled);

        protected TriggerBase(StringStream args, RoleEnum userRole)
        {
            Args = args;
            UserRole = userRole;
        }

        protected TriggerBase(string args, RoleEnum userRole)
        {
            Args = new StringStream(args);
            UserRole = userRole;
        }

        public StringStream Args
        {
            get;
            private set;
        }

        public RoleEnum UserRole
        {
            get;
            private set;
        }

        public CommandBase BindedCommand
        {
            get;
            private set;
        }

        internal Dictionary<string, IParameter> CommandsParametersByName
        {
            get;
            private set;
        }

        internal Dictionary<string, IParameter> CommandsParametersByShortName
        {
            get;
            private set;
        }

        /// <summary>
        ///   Replies accordingly with the given text.
        /// </summary>
        public abstract void Reply(string text);

        public void Reply(string format, params object[] args)
        {
            Reply(string.Format(format, args));
        }

        public void ReplyError(string message)
        {
            Reply("(Error) " + message);
        }

        public void ReplyError(string format, params object[] args)
        {
            Reply(string.Format("(Error) " + format, args));
        }

        public virtual T Get<T>(string name)
        {
            if (CommandsParametersByName.ContainsKey(name))
                return (T) CommandsParametersByName[name].Value;
            if (CommandsParametersByShortName.ContainsKey(name))
                return (T) CommandsParametersByShortName[name].Value;

            throw new ArgumentException("'" + name + "' is not an existing parameter");
        }

        /// <summary>
        /// Returns true only if the argument as been set by the user
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual bool IsArgumentDefined(string name)
        {
            IParameter parameter;
            if (CommandsParametersByName.ContainsKey(name))
            {
                return CommandsParametersByName[name].IsDefined;
            }
            if (CommandsParametersByShortName.ContainsKey(name))
            {
                return CommandsParametersByShortName[name].IsDefined;
            }

            return false;
        }

        /// <summary>
        /// Bind the trigger to a command instance and initialize his parameters. Returns false whenever an error occurs during the initialization
        /// </summary>
        internal bool BindToCommand(CommandBase command)
        {
            BindedCommand = command;

            if (command is SubCommandContainer) // SubCommandContainer has no params
                return true;

            var definedParam = new List<IParameter>();
            var paramToDefine = new List<IParameterDefinition>(BindedCommand.Parameters);

            string word = Args.NextWord();
            while (!string.IsNullOrEmpty(word) && definedParam.Count < BindedCommand.Parameters.Count)
            {
                if (word.StartsWith("\"") && word.EndsWith("\""))
                    word = word.Remove(word.Length - 1, 1).Remove(0, 1);


                if (word.StartsWith("-"))
                {
                    string name = null;
                    string value = null;
                    Match matchIsNamed = m_regexIsNamed.Match(word);
                    if (matchIsNamed.Success)
                    {
                        name = matchIsNamed.Groups[1].Value;
                        value = matchIsNamed.Groups[2].Value;
                    }
                    else
                    {
                        Match matchVar = m_regexVar.Match(word);
                        if (matchVar.Success)
                        {
                            name = matchIsNamed.Groups[1].Value;
                            value = string.Empty;
                        }
                    }

                    if (!string.IsNullOrEmpty(name)) // if one of both regex success
                    {
                        IParameterDefinition definition =
                            paramToDefine.Where(entry => CompareParameterName(entry, name, CommandBase.IgnoreCommandCase)).SingleOrDefault();

                        if (definition == null)
                        {
                            ReplyError("Unknown parameter : {0}", word);
                            return false;
                        }

                        IParameter parameter = definition.CreateParameter();

                        try
                        {
                            parameter.SetValue(value, this);
                        }
                        catch (ConverterException)
                        {
                            ReplyError("Cannot convert : {0} to {1}", word, definition.ValueType);
                            return false;
                        }
                        catch
                        {
                            ReplyError("Cannot parse : {0}", word);
                            return false;
                        }

                        definedParam.Add(parameter);
                        paramToDefine.Remove(definition);
                    }
                }


                else
                {
                    IParameterDefinition definition = paramToDefine.First();

                    IParameter parameter = definition.CreateParameter();

                    try
                    {
                        parameter.SetValue(word, this);
                    }
                    catch
                    {
                        ReplyError("Cannot parse : {0}", word);
                        return false;
                    }

                    definedParam.Add(parameter);
                    paramToDefine.Remove(definition);
                }

                word = Args.NextWord();
            }


            foreach (var unusedDefinition in paramToDefine)
            {
                if (!unusedDefinition.IsOptional)
                {
                    ReplyError("{0} is not defined", unusedDefinition.Name);
                    return false;
                }

                var parameter = unusedDefinition.CreateParameter();

                parameter.SetValue(string.Empty, this);
                definedParam.Add(parameter);
            }

            CommandsParametersByName = definedParam.ToDictionary(entry => entry.Definition.Name);
            CommandsParametersByShortName = definedParam.ToDictionary(entry =>
                    !string.IsNullOrEmpty(entry.Definition.ShortName) ?
                        entry.Definition.ShortName : entry.Definition.Name);
            return true;
        }

        public static bool CompareParameterName(IParameterDefinition parameter, string name, bool useCase)
        {
            return name.Equals(parameter.Name,
                               useCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture)
                   || name.Equals(parameter.ShortName,
                               useCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture);
        }
    }
}