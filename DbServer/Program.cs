using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.CS.Config;
using Db4objects.Db4o.Messaging;
using DbShared;

namespace DbServer
{
    class Program
    {
        static void Main(string[] args)
        {
            new TheServer().Start();
        }
    }

    public class TheServer : IMessageRecipient
    {
        private bool _stop = false;

        public void Start()
        {
            lock(this)
            {
                IServerConfiguration serverConfiguration = Db4oClientServer.NewServerConfiguration();
                serverConfiguration.Networking.MessageRecipient = this;
                IObjectServer server = Db4oClientServer.OpenServer(DbConfig.DBNAME, DbConfig.PORT);
                server.GrantAccess(DbConfig.USER,DbConfig.PASSWORD);

                while(!_stop)
                {
                    Monitor.Wait(this);
                }
            }
        }

        public void ProcessMessage(IMessageContext context, object message)
        {
            if (message is StopServer)
            {
                lock(this)
                {
                    _stop = true;
                    Monitor.PulseAll(this);
                }
            }
        }
    }
}
