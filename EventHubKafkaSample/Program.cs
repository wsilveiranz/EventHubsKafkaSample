using System;
using System.Configuration;

namespace EventHubKafkaSample
{
    class Program
    {
        public static void Main(string[] args)
        {
            string brokerList = ConfigurationManager.AppSettings["eventHubsNamespaceURL"];
            string password = ConfigurationManager.AppSettings["eventHubsConnStr"];
            string topicName = ConfigurationManager.AppSettings["eventHubName"];
            string caCertLocation = ConfigurationManager.AppSettings["caCertLocation"];

            Worker.Producer(brokerList, password, topicName, caCertLocation).Wait();
            Console.ReadKey();

        }
    }
}
