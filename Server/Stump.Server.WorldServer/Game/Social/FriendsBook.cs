﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using NLog;
using Stump.Core.Attributes;
using Stump.DofusProtocol.Enums;
using Stump.Server.WorldServer.Database.Accounts;
using Stump.Server.WorldServer.Game.Accounts;
using Stump.Server.WorldServer.Game.Actors.RolePlay.Characters;
using Stump.Server.WorldServer.Handlers.Basic;
using Stump.Server.WorldServer.Handlers.Characters;
using Stump.Server.WorldServer.Handlers.Friends;

namespace Stump.Server.WorldServer.Game.Social
{
    public class FriendsBook : IDisposable
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        [Variable(true)] public static int MaxFriendsNumber = 30;

        private readonly ConcurrentDictionary<int, Friend> m_friends = new ConcurrentDictionary<int, Friend>();
        private readonly ConcurrentDictionary<int, Ignored> m_ignoreds = new ConcurrentDictionary<int, Ignored>();
        private readonly ConcurrentStack<AccountRelation> m_relationsToRemove = new ConcurrentStack<AccountRelation>();
        private ConcurrentDictionary<int, AccountRelation> m_relations;

        public FriendsBook(Character owner)
        {
            Owner = owner;
        }

        public Character Owner
        {
            get;
            set;
        }

        public IEnumerable<Friend> Friends
        {
            get { return m_friends.Values; }
        }

        public IEnumerable<Ignored> Ignoreds
        {
            get { return m_ignoreds.Values; }
        }

        public bool WarnOnConnection
        {
            get { return Owner.Record.WarnOnConnection; }
            set
            {
                Owner.Record.WarnOnConnection = value;
                FriendHandler.SendFriendWarnOnConnectionStateMessage(Owner.Client, value);
            }
        }

        public bool WarnOnLevel
        {
            get { return Owner.Record.WarnOnLevel; }
            set
            {
                Owner.Record.WarnOnLevel = value;
                FriendHandler.SendFriendWarnOnLevelGainStateMessage(Owner.Client, value);
            }
        }

        public void Dispose()
        {
            World.Instance.CharacterJoined -= OnCharacterLogIn;
        }

        public ListAddFailureEnum? CanAddFriend(WorldAccount friendAccount)
        {
            if (friendAccount.Id == Owner.Client.WorldAccount.Id)
                return ListAddFailureEnum.LIST_ADD_FAILURE_EGOCENTRIC;

            if (IsIgnored(friendAccount.Id) || IsFriend(friendAccount.Id))
                return ListAddFailureEnum.LIST_ADD_FAILURE_IS_DOUBLE;

            if (m_friends.Count >= MaxFriendsNumber)
                return ListAddFailureEnum.LIST_ADD_FAILURE_OVER_QUOTA;

            return null;
        }

        public bool IsFriend(int accountId)
        {
            return Friends.ToArray().FirstOrDefault(x => x.Account.Id == accountId) != null;
        }

        public bool IsIgnored(int accountId)
        {
            return Ignoreds.ToArray().FirstOrDefault(x => x.Account.Id == accountId) != null;
        }

        public bool AddFriend(WorldAccount friendAccount)
        {
            var result = CanAddFriend(friendAccount);

            if (result != null)
            {
                FriendHandler.SendFriendAddFailureMessage(Owner.Client, result.Value);
                return false;
            }

            var relation = new AccountRelation
            {
                AccountId = Owner.Client.Account.Id,
                TargetId = friendAccount.Id,
                Type = AccountRelationType.Friend
            };

            m_relations.AddOrUpdate(relation.TargetId, relation, (key, value) =>
            {
                value.Type = AccountRelationType.Friend;
                return value;
            });

            Friend friend;
            var isConnected = friendAccount.ConnectedCharacter.HasValue;
            if (isConnected)
            {
                var character = World.Instance.GetCharacter(friendAccount.ConnectedCharacter.Value);
                friend = new Friend(relation, friendAccount, character);
            }
            else
                friend = new Friend(relation, friendAccount);

            var success = m_friends.TryAdd(friendAccount.Id, friend);

            if (success && isConnected)
                OnFriendOnline(friend);

            FriendHandler.SendFriendAddedMessage(Owner.Client, friend);

            return success;
        }

        public bool RemoveFriend(Friend friend)
        {
            if (friend.IsOnline())
                OnCharacterLogout(friend.Character); // unregister the events

            Friend dummy;
            if (m_friends.TryRemove(friend.Account.Id, out dummy))
            {
                m_relationsToRemove.Push(friend.Relation);
                FriendHandler.SendFriendDeleteResultMessage(Owner.Client, true, friend.Account.Nickname);

                return true;
            }
            FriendHandler.SendFriendDeleteResultMessage(Owner.Client, false, friend.Account.Nickname);

            return false;
        }

        public ListAddFailureEnum? CanAddIgnored(WorldAccount ignoredAccount)
        {
            if (ignoredAccount.Id == Owner.Client.WorldAccount.Id)
                return ListAddFailureEnum.LIST_ADD_FAILURE_EGOCENTRIC;

            if (IsFriend(ignoredAccount.Id) || IsIgnored(ignoredAccount.Id))
                return ListAddFailureEnum.LIST_ADD_FAILURE_IS_DOUBLE;

            if (m_ignoreds.Count >= MaxFriendsNumber)
                return ListAddFailureEnum.LIST_ADD_FAILURE_OVER_QUOTA;

            return null;
        }

        public bool AddIgnored(WorldAccount ignoredAccount, bool session = false)
        {
            var result = CanAddIgnored(ignoredAccount);

            if (result != null)
            {
                FriendHandler.SendIgnoredAddFailureMessage(Owner.Client, result.Value);
                return false;
            }

            var relation = new AccountRelation
            {
                AccountId = Owner.Client.Account.Id,
                TargetId = ignoredAccount.Id,
                Type = AccountRelationType.Ignored
            };

            if (!session)
                m_relations.AddOrUpdate(relation.TargetId, relation, (key, value) =>
                {
                    value.Type = AccountRelationType.Ignored;
                    return value;
                });

            Ignored ignored;
            if (ignoredAccount.ConnectedCharacter.HasValue)
            {
                var character = World.Instance.GetCharacter(ignoredAccount.ConnectedCharacter.Value);
                ignored = new Ignored(relation, ignoredAccount, session, character);
            }
            else
            {
                ignored = new Ignored(relation, ignoredAccount, session);
            }
                
            var success = m_ignoreds.TryAdd(ignoredAccount.Id, ignored);
            FriendHandler.SendIgnoredAddedMessage(Owner.Client, ignored, session);

            return success;
        }

        public bool RemoveIgnored(Ignored ignored)
        {
            Ignored dummy;
            if (m_ignoreds.TryRemove(ignored.Account.Id, out dummy))
            {
                m_relationsToRemove.Push(ignored.Relation);
                FriendHandler.SendIgnoredDeleteResultMessage(Owner.Client, true, ignored.Session,
                    ignored.Account.Nickname);

                return true;
            }
            FriendHandler.SendIgnoredDeleteResultMessage(Owner.Client, false, ignored.Session,
                ignored.Account.Nickname);

            return false;
        }

        private void OnCharacterLogIn(Character character)
        {
            Friend friend;
            if (m_friends.TryGetValue(character.Client.WorldAccount.Id, out friend))
            {
                friend.SetOnline(character);
                OnFriendOnline(friend);

                if (WarnOnConnection)
                    // %1 ({player,%2,%3}) est en ligne.
                    BasicHandler.SendTextInformationMessage(Owner.Client,
                        TextInformationTypeEnum.TEXT_INFORMATION_MESSAGE, 143,
                        character.Client.WorldAccount.Nickname, character.Name);
            }
            Ignored ignored;
            if (m_ignoreds.TryGetValue(character.Client.WorldAccount.Id, out ignored))
                ignored.SetOnline(character);
        }

        private void OnFriendOnline(Friend friend)
        {
            friend.Character.LoggedOut += OnCharacterLogout;
            friend.Character.LevelChanged += OnLevelChanged;
            friend.Character.ContextChanged += OnContextChanged;
        }

        private void OnContextChanged(Character character, bool infight)
        {
            var friend = TryGetFriend(character);

            if (friend == null)
            {
                logger.Error("Sad, friend bound with character {0} is not found :(", character);
                return;
            }

            FriendHandler.SendFriendUpdateMessage(Owner.Client, friend);
        }

        private void OnLevelChanged(Character character, byte currentlevel, int difference)
        {
            var friend = TryGetFriend(character);

            if (friend == null)
            {
                logger.Error("Sad, friend bound with character {0} is not found :(", character);
                return;
            }

            FriendHandler.SendFriendUpdateMessage(Owner.Client, friend);

            if (!WarnOnLevel)
                return;

            if (character.Map != Owner.Map)
                CharacterHandler.SendCharacterLevelUpInformationMessage(Owner.Client, character, character.Level);
        }

        private void OnCharacterLogout(Character character)
        {
            var friend = TryGetFriend(character);

            if (friend == null)
                logger.Error("Sad, friend bound with character {0} is not found :(", character);
            else
                friend.SetOffline();

            character.LoggedOut -= OnCharacterLogout;
            character.LevelChanged -= OnLevelChanged;
            character.ContextChanged -= OnContextChanged;
        }

        public void Load()
        {
            m_relations =
                new ConcurrentDictionary<int, AccountRelation>(WorldServer.Instance.DBAccessor.Database
                                                                          .Query<AccountRelation>(
                                                                              string.Format(
                                                                                  AccountRelationRelator.FetchByAccount,
                                                                                  Owner.Account.Id))
                                                                          .ToDictionary(x => x.TargetId, x => x));

            foreach (var relation in m_relations.Values)
            {
                var account = World.Instance.GetConnectedAccount(relation.TargetId) ??
                              AccountManager.Instance.FindById(relation.TargetId);

                // doesn't exist anymore, so we delete it
                if (account == null)
                {
                    WorldServer.Instance.DBAccessor.Database.Delete(relation);
                    continue;
                }

                switch (relation.Type)
                {
                    case AccountRelationType.Friend:
                        if (account.ConnectedCharacter.HasValue)
                        {
                            var character = World.Instance.GetCharacter(account.ConnectedCharacter.Value);

                            m_friends.TryAdd(account.Id, new Friend(relation, account, character));
                        }
                        else
                            m_friends.TryAdd(account.Id, new Friend(relation, account));
                        break;
                    case AccountRelationType.Ignored:
                        if (account.ConnectedCharacter.HasValue)
                        {
                            var character = World.Instance.GetCharacter(account.ConnectedCharacter.Value);

                            m_ignoreds.TryAdd(account.Id, new Ignored(relation, account, false, character));
                        }
                        else
                            m_ignoreds.TryAdd(account.Id, new Ignored(relation, account, false));
                        break;
                }
            }

            World.Instance.CharacterJoined += OnCharacterLogIn;
        }

        public void Save()
        {
            var database = WorldServer.Instance.DBAccessor.Database;
            foreach (var relation in m_relations)
            {
                database.Save(relation.Value);
            }
            while (m_relationsToRemove.Count != 0)
            {
                AccountRelation relation;
                if (m_relationsToRemove.TryPop(out relation))
                    database.Delete(relation);
            }
        }

        public Friend TryGetFriend(Character character)
        {
            Friend friend;
            return m_friends.TryGetValue(character.Account.Id, out friend) ? friend : null;
        }
    }
}