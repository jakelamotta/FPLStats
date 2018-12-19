using Common;
using ServiceInstance;
using System;
using System.Collections.Generic;

namespace FantasyStats
{
    class Program
    {
        static void Main(string[] args)
        {
            var instance = new ServiceInstance.ServiceInstance();
            instance.Run();
        }
    }
}
