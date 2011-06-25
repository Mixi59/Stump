
using System.Collections.Generic;
using System.Linq;
using Stump.DofusProtocol.Classes;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.Server.WorldServer.Global;

namespace Stump.Server.WorldServer.Handlers
{
    public class FriendHandler : WorldHandlerContainer
    {
        [WorldHandler(typeof (IgnoredGetListMessage))]
        public static void HandleIgnoredGetListMessage(WorldClient client, IgnoredGetListMessage message)
        {
            //  var ignoreds = client.WorldAccount.Ignored.Select(i => new IgnoredInformations(i.Nickname, i.Id));

            //SendIgnoredListMessage(client, ignoreds);
        }

        [WorldHandler(typeof (FriendsGetListMessage))]
        public static void HandleFriendsGetListMessage(WorldClient client, FriendsGetListMessage message)
        {
            IEnumerable<FriendInformations> friends = client.WorldAccount.Friends.Select(f =>
                                                                                         new FriendInformations(
                                                                                             f.Nickname,
                                                                                             (uint)
                                                                                             PlayerStateEnum.
                                                                                                 NOT_CONNECTED,
                                                                                             f.LastConnectionTimeStamp));

            SendFriendsListMessage(client, friends);
        }


        [WorldHandler(typeof (FriendSetWarnOnConnectionMessage))]
        public static void HandleFriendSetWarnOnConnectionMessage(WorldClient client,
                                                                  FriendSetWarnOnConnectionMessage message)
        {
            client.WarnOnFriendConnection = message.enable;
        }

        [WorldHandler(typeof (FriendSetWarnOnLevelGainMessage))]
        public static void HandleFriendSetWarnOnLevelGainMessage(WorldClient client,
                                                                 FriendSetWarnOnLevelGainMessage message)
        {
            client.WarnOnFriendLevelGain = message.enable;
        }

        [WorldHandler(typeof (FriendAddRequestMessage))]
        public static void HandleFriendAddRequestMessage(WorldClient client, FriendAddRequestMessage message)
        {
            if (message.name == client.ActiveCharacter.Name)
            {
                client.Send(new FriendAddFailureMessage((uint) ListAddFailureEnum.LIST_ADD_FAILURE_EGOCENTRIC));
                return;
            }

            var account =
                World.Instance.GetCharacter(message.name).Client.WorldAccount;

            if (account == null)
            {
                client.Send(new FriendAddFailureMessage((uint) ListAddFailureEnum.LIST_ADD_FAILURE_NOT_FOUND));
                return;
            }

            if (client.WorldAccount.Friends.Contains(account))
            {
                client.Send(new FriendAddFailureMessage((uint) ListAddFailureEnum.LIST_ADD_FAILURE_IS_DOUBLE));
                return;
            }

            if (client.WorldAccount.Friends.Count >= 100)
            {
                client.Send(new FriendAddFailureMessage((uint) ListAddFailureEnum.LIST_ADD_FAILURE_OVER_QUOTA));
                return;
            }

            client.WorldAccount.Friends.Add(account);
            client.Send(
                new FriendAddedMessage(new FriendInformations(account.Nickname,
                                                              (uint) PlayerStateEnum.GAME_TYPE_FIGHT,
                                                              account.LastConnectionTimeStamp)));
        }


        public static void SendIgnoredListMessage(WorldClient client, IEnumerable<IgnoredInformations> ignoreds)
        {
            client.Send(new IgnoredListMessage(ignoreds.ToList()));
        }

        public static void SendFriendsListMessage(WorldClient client, IEnumerable<FriendInformations> friends)
        {
            client.Send(new FriendsListMessage(friends.ToList()));
        }
    }
}