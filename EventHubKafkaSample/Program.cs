using System;

namespace EventHubKafkaSample
{
    class Program
    {
        public static void Main(string[] args)
        {
            Worker.Producer().Wait();
            Console.ReadKey();

        }
    }
}
