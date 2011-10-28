
using System.Net;
using System.Net.Sockets;
using Stump.Server.BaseServer.Network;

namespace Stump.Tools.Proxy.Network
{
    public class AuthClient : ProxyClient
    {
        public AuthClient(Socket socket, IPEndPoint ipEndPoint)
            : base(socket)
        {
            IsInCriticalZone = true;

            BindToServer(ipEndPoint);
        }


        protected override SocketAsyncEventArgs PopWriteSocketAsyncArgs()
        {
            return Proxy.Instance.AuthClientManager.PopWriteSocketAsyncArgs();
        }

        protected override void PushWriteSocketAsyncArgs(SocketAsyncEventArgs args)
        {
            Proxy.Instance.AuthClientManager.PushWriteSocketAsyncArgs(args);
        }

        protected override SocketAsyncEventArgs PopReadSocketAsyncArgs()
        {
            return Proxy.Instance.AuthClientManager.PopReadSocketAsyncArgs();
        }

        protected override void PushReadSocketAsyncArgs(SocketAsyncEventArgs args)
        {
            Proxy.Instance.AuthClientManager.PushReadSocketAsyncArgs(args);
        }

    }
}