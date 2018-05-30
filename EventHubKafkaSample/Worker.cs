using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Net.Http;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace EventHubKafkaSample
{
    class Worker
    {
        public static async Task Producer()
        {
            try
            {

                string brokerList = ConfigurationManager.AppSettings["eventHubsNamespaceURL"];
                string password = ConfigurationManager.AppSettings["eventHubsConnStr"];
                string topicName = ConfigurationManager.AppSettings["eventHubName"];
                string caCertLocation = ConfigurationManager.AppSettings["caCertLocation"];


                var config = new Dictionary<string, object> {
                    { "bootstrap.servers", brokerList },
                    { "security.protocol","SASL_SSL" },
                    { "sasl.mechanism","PLAIN" },
                    { "ssl.ca.location",caCertLocation },
                    { "sasl.username", "$ConnectionString"},
                    { "sasl.password", password },
                    { "broker.version.fallback ","0.10.0.0" },
                    { "api.version.fallback.ms","0" }
                    { "debug", "security,broker,protocol" }
                };

                using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
                {
                    Console.WriteLine("Initiating Execution");
                    for (int x = 0; x < 100; x++)
                    {
                        var msg = string.Format("This is a sample message - msg # {0} at {1}", x, DateTime.Now.ToString('yyyMMdd_HHmmSSfff'));
                        var deliveryReport = await producer.ProduceAsync(topicName, null, msg);
                        Console.WriteLine(string.Format("Message {0} sent.", x));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Exception Ocurred - {0}", e.Message));
            }
        }
    }
}
