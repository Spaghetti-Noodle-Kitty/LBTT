using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LBTT_Package
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress localIP = null;

            if (args.Length >= 2)
            {
                foreach (string arg in args)
                {
                    if (IPAddress.TryParse(arg, out localIP))
                    {
                        Console.WriteLine("IP Argument detected, using " + localIP.ToString());
                    }
                    if (arg == "-c")
                    {
                        Console.WriteLine("Using Client Role");
                        ClientRole.InitClientRole(localIP);
                    }
                    if (arg == "-b")
                    {
                        Console.WriteLine("Using Broker Role");
                        BrokerRole.InitBrokerRole(localIP);
                    }
                    if (arg == "-s")
                    {
                        Console.WriteLine("Using Sensor Role");
                    }
                }
            }
            else
            {
                Console.WriteLine("LBTT Help");
                Console.WriteLine("LBTT_package.exe [IP] [arguments]");
                Console.WriteLine("[IP]");
                Console.WriteLine("    > The IP this local instance of LBTT should use");
                Console.WriteLine("[arguments]");
                Console.WriteLine("    > -c  Set this instance to ClientRole");
                Console.WriteLine("    > -b  Set this instance to BrokerRole");
                Console.WriteLine("    > -s  Set this instance to SensorRole");
            }
        }
    }
}
