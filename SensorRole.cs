using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LBTT_Package
{
    class SensorRole
    {
        public static void InitSensorRole(IPAddress IP)
        {
            Console.OutputEncoding = Encoding.UTF8;

            byte[] data = new byte[1024];

            string input, stringData;

            IPEndPoint ipep = new IPEndPoint(IP, 9050);

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                server.Connect(ipep);

            }
            catch (SocketException e)
            {
                Console.WriteLine("Unable to connect to broker.");

                Console.WriteLine(e.ToString());

                return;
            }

            int recv = server.Receive(data);

            stringData = Encoding.UTF8.GetString(data, 0, recv);

            Console.WriteLine(stringData);

            while (true)
            {
                input = Console.ReadLine();

                if (input == "exit")

                    break;

                Console.WriteLine("You: " + input);

                server.Send(Encoding.UTF8.GetBytes(input));

                data = new byte[1024];

                recv = server.Receive(data);

                stringData = Encoding.UTF8.GetString(data, 0, recv);

                byte[] utf8string = System.Text.Encoding.UTF8.GetBytes(stringData);

                Console.WriteLine("Server: " + stringData);
            }

            Console.WriteLine("Disconnecting from server...");

            server.Shutdown(SocketShutdown.Both);

            server.Close();

            Console.WriteLine("Disconnected!");

            Console.ReadLine();
        }
    }
}
