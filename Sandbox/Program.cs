using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Db4objects.Db4o;
using Db4objects.Db4o.CS;
using Db4objects.Db4o.CS.Config;
using Db4objects.Db4o.Linq;
using DbShared;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            // local
            Console.WriteLine("Running with local database (embedded)");

            using (Db4oSample sample = new Db4oSample(false))
            {
                sample.Run();
            }

            Console.WriteLine("Local test complete - hit any key to continue");
            Console.ReadKey();

            // client server
            Console.WriteLine("**********  Start server, hit key  **********");
            Console.ReadKey();

            Console.WriteLine("Running with client/server model");

            using (Db4oSample sample = new Db4oSample(true))
            {
                sample.Run();
            }

            Console.WriteLine("Client/Server test complete - hit any key to continue");
            Console.ReadKey();
        }

    }
}
