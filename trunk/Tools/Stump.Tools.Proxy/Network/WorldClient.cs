
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Messages;
using Stump.DofusProtocol.Types;
using Stump.Server.BaseServer.Network;
using Stump.Server.WorldServer.Database.Npcs;
using Stump.Server.WorldServer.Worlds.Maps;

namespace Stump.Tools.Proxy.Network
{
    public class WorldClient : ProxyClient
    {
        private static readonly Dictionary<string, SelectedServerDataMessage> m_tickets = new Dictionary<string, SelectedServerDataMessage>();

        public static void PushTicket(string ticket, SelectedServerDataMessage server)
        {
            m_tickets.Add(ticket, server);
        }

        public static SelectedServerDataMessage PopTicket(string ticket)
        {
            if (!m_tickets.ContainsKey(ticket))
                return null;

            var result = m_tickets[ticket];

            m_tickets.Remove(ticket);

            return result;
        }

        public WorldClient(Socket socket)
            : base(socket)
        {
            MapNpcs = new Dictionary<int, GameRolePlayNpcInformations>();
            MapIOs = new Dictionary<int, InteractiveElement>();

            Send(new ProtocolRequired(VersionExtension.ProtocolRequired, VersionExtension.ActualProtocol));
            Send(new HelloGameMessage());
        }

        protected override SocketAsyncEventArgs PopWriteSocketAsyncArgs()
        {
            return Proxy.Instance.WorldClientManager.PopWriteSocketAsyncArgs();
        }

        protected override void PushWriteSocketAsyncArgs(SocketAsyncEventArgs args)
        {
            Proxy.Instance.WorldClientManager.PushWriteSocketAsyncArgs(args);
        }

        protected override SocketAsyncEventArgs PopReadSocketAsyncArgs()
        {
            return Proxy.Instance.WorldClientManager.PopReadSocketAsyncArgs();
        }

        protected override void PushReadSocketAsyncArgs(SocketAsyncEventArgs args)
        {
            Proxy.Instance.WorldClientManager.PushReadSocketAsyncArgs(args);
        }

        public string Ticket
        {
            get;
            set;
        }

        public CharacterBaseInformations CharacterInformations
        {
            get;
            set;
        }

        private NpcDialogReplyMessage m_guessNpcReply;

        public NpcDialogReplyMessage GuessNpcReply
        {
            get { return m_guessNpcReply; }
            set
            {
                LastNpcReply = m_guessNpcReply;

                m_guessNpcReply = value;
            }
        }

        public NpcDialogReplyMessage LastNpcReply
        {
            get;
            set;
        }

        public NpcMessage LastNpcMessage
        {
            get;
            set;
        }

        public NpcGenericActionRequestMessage GuessNpcFirstAction
        {
            get;
            set;
        }

        public Tuple<Map, InteractiveUseRequestMessage, InteractiveUsedMessage> GuessSkillAction
        {
            get;
            set;
        }

        public Dictionary<int, GameRolePlayNpcInformations> MapNpcs
        {
            get;
            set;
        }

        public Dictionary<int, InteractiveElement> MapIOs
        {
            get;
            set;
        }

        public Map LastMap
        {
            get;
            set;
        }

        private Map m_currentMap;

        public Map CurrentMap
        {
            get { return m_currentMap; }
            set
            {
                LastMap = m_currentMap;
                
                m_currentMap = value;
            }
        }

        public ushort? GuessCellTrigger
        {
            get;
            set;
        }

        private EntityDispositionInformations m_disposition;

        public EntityDispositionInformations Disposition
        {
            get { return m_disposition; }
            set
            {
                m_disposition = value;

                if (m_delegateToCall != null)
                {
                    m_delegateToCall.DynamicInvoke();

                    m_delegateToCall = null;
                }
            }
        }

        public bool GuessAction
        {
            get
            {
                return GuessNpcReply != null || GuessNpcFirstAction != null || GuessSkillAction != null || GuessCellTrigger != null;
            }
        }

        private Action m_delegateToCall;

        public void CallWhenTeleported(Action action)
        {
            m_delegateToCall = action;
        }

        public void SendChatMessage(string message)
        {
            SendChatMessage(message, Color.BlueViolet);
        }

        public void SendChatMessage(string message, Color color)
        {
            Send(new ChatServerMessage(
                            (sbyte)ChatActivableChannelsEnum.PSEUDO_CHANNEL_INFO,
                            "<font color=\"#" + color.ToArgb().ToString("X") + "\">" + "[PROXY] : " + message + "</font>",
                            0,
                            "",
                            0,
                            "",
                            0));
        }
    }
}