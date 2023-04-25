using System;

namespace Server
{
    public class Program
    {
        public static void Main()
        {
            Server server = new();

            server.Start();
        }
    }
}