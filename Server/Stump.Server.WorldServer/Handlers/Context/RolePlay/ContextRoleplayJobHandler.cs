﻿using System.Linq;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Handlers.Context.RolePlay
{
    public partial class ContextRoleplay
    {
        public static void SendJobExperienceMultiUpdateMessage(IPacketReceiver client, Character character)
        {
            client.Send(new JobExperienceMultiUpdateMessage(character.Jobs.Select(x => x.GetJobExperience())));
        }
    }
}