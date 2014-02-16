using System.Collections.Generic;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Game.Actors.RolePlay;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Npcs;
using Stump.Server.WorldServer.Game.Actors.RolePlay.TaxCollectors;

namespace Stump.Server.WorldServer.Handlers.Context.RolePlay
{
    public partial class ContextRoleplayHandler
    {
        [WorldHandler(NpcGenericActionRequestMessage.Id)]
        public static void HandleNpcGenericActionRequestMessage(WorldClient client, NpcGenericActionRequestMessage message)
        {
            var npc = client.Character.Map.GetActor<RolePlayActor>(message.npcId);

            if (npc == null)
                return;

            if (npc is Npc)
                (npc as Npc).InteractWith((NpcActionTypeEnum) message.npcActionId, client.Character);
            else if (npc is TaxCollectorNpc)
                SendTaxCollectorDialogQuestionExtendedMessage(client, (npc as TaxCollectorNpc));
        }

        [WorldHandler(NpcDialogReplyMessage.Id)]
        public static void HandleNpcDialogReplyMessage(WorldClient client, NpcDialogReplyMessage message)
        {
            client.Character.ReplyToNpc(message.replyId);
        }

        public static void SendNpcDialogCreationMessage(IPacketReceiver client, Npc npc)
        {
            client.Send(new NpcDialogCreationMessage(npc.Position.Map.Id, npc.Id));
        }

        public static void SendNpcDialogQuestionMessage(IPacketReceiver client, NpcMessage message, IEnumerable<short> replies, params string[] parameters)
        {
            client.Send(new NpcDialogQuestionMessage((short)message.Id, parameters, replies));
        }

        public static void SendTaxCollectorDialogQuestionExtendedMessage(IPacketReceiver client, TaxCollectorNpc taxCollector)
        {
            client.Send(new NpcDialogCreationMessage(taxCollector.Map.Id, taxCollector.Id));
            client.Send(new TaxCollectorDialogQuestionExtendedMessage(taxCollector.Guild.GetBasicGuildInformations(), (short) taxCollector.Guild.TaxCollectorPods,
                (short) taxCollector.Guild.TaxCollectorProspecting, (short)taxCollector.Guild.TaxCollectorWisdom, (sbyte)taxCollector.Guild.TaxCollectors.Count, -1, 0, 0, 0, 0));
        }
    }
}