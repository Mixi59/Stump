﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NLog;
using Stump.Core.Reflection;
using Stump.DofusProtocol.Enums;

namespace Stump.Server.BaseServer.Commands
{
    public class CommandManager : Singleton<CommandManager>
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private IDictionary<string, CommandBase> m_commandsByAlias;
        private readonly List<CommandBase> m_registeredCommands;
        private readonly List<Type> m_registeredTypes;

        private CommandManager()
        {
            m_commandsByAlias = new Dictionary<string, CommandBase>();
            m_registeredCommands = new List<CommandBase>();
            m_registeredTypes = new List<Type>();
        }

        /// <summary>
        /// Regroup all CommandBase and SubCommandContainer by alias
        /// </summary>
        public IDictionary<string, CommandBase> CommandsByAlias
        {
            get { return m_commandsByAlias; }
        }

        /// <summary>
        /// Regroup all CommandBases, SubCommandContainers and SubCommands
        /// </summary>
        public IEnumerable<CommandBase> AvailableCommands
        {
            get
            {
                return m_registeredCommands;
            }
        }

        #region Get Method

        public CommandBase GetCommand(string alias)
        {
            CommandBase command;
            m_commandsByAlias.TryGetValue(alias, out command);

            return command;
        }

        public CommandBase this[string alias]
        {
            get { return GetCommand(alias); }
        }

        #endregion

        #region Register Methods

        public void RegisterAll(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            var callTypes = assembly.GetTypes().Where(entry => !entry.IsAbstract);

            foreach (Type type in callTypes)
            {
                if (!IsCommandRegister(type))
                    RegisterCommand(type);
            }

            SortCommands();
        }

        private void SortCommands()
        {
            m_commandsByAlias = m_commandsByAlias.OrderBy(entry => entry.Key).ToDictionary(entry => entry.Key,
                                                                               entry => entry.Value);

            foreach (var availableCommand in AvailableCommands.OfType<SubCommandContainer>())
            {
                availableCommand.SortSubCommands();
            }
        }

        public void RegisterCommand(Type commandType)
        {
            if (commandType.IsSubclassOf(typeof(SubCommand)))
            {
                RegisterSubCommand(commandType);
            }
            else if (commandType.IsSubclassOf(typeof(SubCommandContainer)))
            {
                RegisterSubCommandContainer(commandType);
            }
            else if (commandType.IsSubclassOf(typeof(CommandBase)))
            {
                RegisterCommandBase(commandType);
            }
        }

        private void RegisterCommandBase(Type commandType)
        {
            var command = Activator.CreateInstance(commandType) as CommandBase;

            if (command == null)
                throw new Exception(string.Format("Cannot create a new instance of {0}", commandType));

            if (command.Aliases == null || command.RequiredRole == RoleEnum.None)
            {
                logger.Error(
                    "An error occurred while registering Command : {0}. Either aliases are null or RequiredRole is incorrect.\nPlease check and repair.", commandType.Name);
                return;
            }

            m_registeredCommands.Add(command);
            foreach (string alias in command.Aliases)
            {
                CommandBase value;
                if (!m_commandsByAlias.TryGetValue(alias, out value))
                {
                    m_commandsByAlias[CommandBase.IgnoreCommandCase ? alias.ToLower() : alias] = command;
                    m_registeredTypes.Add(commandType);
                }
                else
                {
                    logger.Error("Found two Commands with Alias \"{0}\": {1} and {2}", alias, value, command);
                }
            }
        }

        private void RegisterSubCommandContainer(Type commandType)
        {
            var command = Activator.CreateInstance(commandType) as SubCommandContainer;

            if (command == null)
                throw new Exception(string.Format("Cannot create a new instance of {0}", commandType));

            if (command.Aliases == null || command.RequiredRole == RoleEnum.None)
            {
                logger.Error(
                    "An error occurred while registering Command : {0}. Either aliases are null or RequiredRole is incorrect.\nPlease check and repair.", commandType.Name);
                return;
            }

            m_registeredCommands.Add(command);
            foreach (string alias in command.Aliases)
            {
                CommandBase value;
                if (!m_commandsByAlias.TryGetValue(alias, out value))
                {
                    m_commandsByAlias[CommandBase.IgnoreCommandCase ? alias.ToLower() : alias] = command as CommandBase;

                    m_registeredTypes.Add(commandType);
                }
                else
                {
                    logger.Error("Found two Commands with Alias \"{0}\": {1} and {2}", alias, value, command);
                }
            }
        }

        private void RegisterSubCommand(Type commandType)
        {
            var subcommand = Activator.CreateInstance(commandType) as SubCommand;

            if (subcommand == null)
                throw new Exception(string.Format("Cannot create a new instance of {0}", commandType));

            if (!IsCommandRegister(subcommand.ParentCommand))
                RegisterCommand(subcommand.ParentCommand);

            var parentCommand = AvailableCommands.Where(entry => entry.GetType() == subcommand.ParentCommand).SingleOrDefault() as SubCommandContainer;

            if (parentCommand == null)
                throw new Exception(string.Format("Cannot found declaration of command '{0}'", subcommand.ParentCommand));

            parentCommand.AddSubCommand(subcommand);
            m_registeredCommands.Add(subcommand);
            m_registeredTypes.Add(commandType);
        }

        public bool IsCommandRegister(Type commandType)
        {
            return m_registeredTypes.Contains(commandType);
        }

        #endregion

        #region Handle Method

        public void HandleCommand(TriggerBase trigger)
        {
            string cmdstring = trigger.Args.NextWord();

            if (CommandBase.IgnoreCommandCase)
                cmdstring = cmdstring.ToLower();

            CommandBase cmd = this[cmdstring];

            if (cmd != null && trigger.UserRole >= cmd.RequiredRole)
            {
                try
                {
                    if (trigger.BindToCommand(cmd))
                        cmd.Execute(trigger);
                }
                catch (Exception ex)
                {
                    trigger.Reply("Raised exception when executing command : " + ex.Message);
                }
            }
            else
            {
                trigger.Reply("Incorrect Command \"{0}\". Type commandslist or help for command list.", cmdstring);
            }
        }

        #endregion
    }
}