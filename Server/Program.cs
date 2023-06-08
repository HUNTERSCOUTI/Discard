using System;

namespace Server
{
    public class Program
    {
        public static void Main()
        {
            Console.Clear();
            Server server = new();

            server.Start();
        }
    }
}