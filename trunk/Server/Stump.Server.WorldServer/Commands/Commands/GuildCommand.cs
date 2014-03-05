﻿using Stump.DofusProtocol.Enums;
using Stump.Server.BaseServer.Commands;
using Stump.Server.WorldServer.Commands.Commands.Patterns;
using Stump.Server.WorldServer.Commands.Trigger;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Game.Dialogs.Guilds;
using Stump.Server.WorldServer.Game.Guilds;

namespace Stump.Server.WorldServer.Commands.Commands
{
    internal class GuildCommand : SubCommandContainer
    {
        public GuildCommand()
        {
            Aliases = new[] {"guild"};
            RequiredRole = RoleEnum.GameMaster;
            Description = "Provides many commands to manage guilds";
        }
    }

    public class GuildCreateCommand : InGameSubCommand
    {
        public GuildCreateCommand()
        {
            Aliases = new[] {"create"};
            RequiredRole = RoleEnum.Administrator;
            ParentCommand = typeof (GuildCommand);
        }


        public override void Execute(GameTrigger trigger)
        {
            var panel = new GuildCreationPanel(trigger.Character);
            panel.Open();
        }
    }

    public class GuildJoinCommand : InGameSubCommand
    {
        public GuildJoinCommand()
        {
            Aliases = new[] {"join"};
            RequiredRole = RoleEnum.GameMaster;
            ParentCommand = typeof (GuildCommand);

            AddParameter<string>("guildname", "guild", "The name of the guild");
        }

        public override void Execute(GameTrigger trigger)
        {
            Character character = trigger.Character;

            if (character.GuildMember == null)
            {
                var guildName = trigger.Get<string>("guildname");
                Guild guild = GuildManager.Instance.TryGetGuild(guildName);

                GuildMember guildMember;
                guild.TryAddMember(character, out guildMember);

                character.GuildMember = guildMember;

                trigger.Reply(string.Format("You have join Guild: {0}", guild.Name));
            }
            else
                trigger.ReplyError("You must leave your Guild before join another");
        }
    }

    public class GuildBossCommand : InGameSubCommand
    {
        public GuildBossCommand()
        {
            Aliases = new[] {"boss"};
            RequiredRole = RoleEnum.GameMaster;
            ParentCommand = typeof (GuildCommand);
        }

        public override void Execute(GameTrigger trigger)
        {
            Character character = trigger.Character;

            if (character.GuildMember != null)
                character.Guild.SetBoss(character.GuildMember);
            else
                trigger.ReplyError("You must be in a guild to do that !");
        }
    }
}