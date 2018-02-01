using System;
using System.Threading;
using AirTransit_Core;

namespace AirTransit_Console
{
    class ConsoleApp
    {
        static void Main(string[] args)
        {
            var messageFetcher = new MessageFetcher((messages) =>
            {
                Thread.Sleep(new TimeSpan(0, 0, 0, 5));
                foreach (var message in messages)
                {
                    Console.WriteLine(message);

                }
            }, new TimeSpan(0,0,0,1));
            
            Console.Read();
        }
    }
}
