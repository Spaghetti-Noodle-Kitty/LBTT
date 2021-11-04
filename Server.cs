using System;
using System.Collections.Generic;
using System.Net;

namespace LBTT_Package
{
    class Server
    {
        public static IPAddress localIP = IPAddress.Parse("10.0.0.20");

        public static List<Subscriber> SubList = new List<Subscriber>();

        public struct Subscriber
        {
            public IPAddress SubscriberIP;
            public String SubscribedHierarchy;
        };

        public static Subscriber AcceptSubscription(IPAddress sub, string hierarchy)
        {
            Subscriber newSub;

            Console.WriteLine("Subscribed {0} to {1}", sub, hierarchy);

            newSub.SubscriberIP = sub;
            newSub.SubscribedHierarchy = hierarchy;

            return newSub;
        }

        public static void AcceptNewSubscriber(IPAddress subscriber, string hierarchy)
        {
            SubList.Add(AcceptSubscription(subscriber, hierarchy));
        }

        public static void SendSubscribtionPush(string Value, string source)
        {
            List<IPAddress> AffectedSubscribers = new List<IPAddress>();

            foreach (Subscriber sub in SubList)
                if (sub.SubscribedHierarchy == source)
                    AffectedSubscribers.Add(sub.SubscriberIP);

            //send value to all affected subscribers


        }
    }
}
