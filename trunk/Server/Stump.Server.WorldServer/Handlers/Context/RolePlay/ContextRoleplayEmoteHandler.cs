using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Core.Network;
using Stump.Server.WorldServer.Worlds.Actors;
using Stump.Server.WorldServer.Worlds.Actors.RolePlay.Characters;

namespace Stump.Server.WorldServer.Handlers.Context.RolePlay
{
    public partial class ContextRoleplayHandler
    {
        [WorldHandler(EmotePlayRequestMessage.Id)]
        public static void HandleEmotePlayRequestMessage(WorldClient client, EmotePlayRequestMessage message)
        {
            // todo : found the duration of each emote
            //client.ActiveCharacter.StartEmote((EmotesEnum) message.emoteId, 0);
        }

        public static void SendEmotePlayMessage(WorldClient client, Character character, EmotesEnum emote, uint duration)
        {
            client.Send(new EmotePlayMessage(
                            (byte) emote,
                            (byte) duration,
                            character.Id,
                            (int) character.Client.Account.Id
                            ));
        }

        public static void SendEmotePlayMessage(WorldClient client, ContextActor actor, EmotesEnum emote, uint duration)
        {
            client.Send(new EmotePlayMessage(
                            (byte) emote,
                            (byte) duration,
                            actor.Id,
                            0
                            ));
        }

        public static void SendEmoteListMessage(WorldClient client, IEnumerable<byte> emoteList)
        {
            client.Send(new EmoteListMessage(emoteList));
        }
    }
}