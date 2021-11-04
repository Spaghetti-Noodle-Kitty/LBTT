using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LBTT_Package
{
    class BrokerRole
    {
        public static void InitBrokerRole(IPAddress IP)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int recv;
            byte[] data = new byte[1024];
            IPEndPoint ipep = new IPEndPoint(IP, 9050);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            newsock.Bind(ipep);
            newsock.Listen(10);

            Console.WriteLine("Waiting for a sensor");

            Socket client = newsock.Accept();
            IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;

            Console.WriteLine("Connected with {0} at port {1}", clientep.Address, clientep.Port);
            string welcome = "LBTT_v1.0 Broker";

            data = Encoding.UTF8.GetBytes(welcome);

            client.Send(data, data.Length, SocketFlags.None);

            string input;

            while (true)
            {

                data = new byte[1024];
                string[] update = { };
                recv = client.Receive(data);
                input = "REJ";

                if (recv == 0)
                    break;


                Console.WriteLine("Client: " + Encoding.UTF8.GetString(data, 0, recv));

                if (Encoding.UTF8.GetString(data, 0, recv).StartsWith("subto "))
                {
                    Server.AcceptNewSubscriber(clientep.Address, Encoding.UTF8.GetString(data, 0, recv).Substring(6));
                    input = "ACK";
                }

                if (Encoding.UTF8.GetString(data, 0, recv).StartsWith("update "))
                {
                    update = Encoding.UTF8.GetString(data, 0, recv).Substring(7).Split('|');
                    Server.SendSubscribtionPush(update[0], update[1]);
                    input = "ACK";
                }

                if (Encoding.UTF8.GetString(data, 0, recv) == ("disconnect"))
                {
                    break;
                }

                client.Send(Encoding.UTF8.GetBytes(input));
            }

            Console.WriteLine("Disconnected from {0}", clientep.Address);

            client.Close();

            newsock.Close();

            Console.ReadLine();
        }
    }
}
