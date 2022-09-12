﻿using ClientServer;
using System;
using System.Threading;

namespace UI
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(2, 2);
            ThreadPool.SetMinThreads(4, 4);
            Server server = new Server("127.0.0.1", 80);
            server.Start();
        }
    }
}
