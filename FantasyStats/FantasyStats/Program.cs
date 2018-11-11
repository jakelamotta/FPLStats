using ServiceInstance;
using System;

namespace FantasyStats
{
    class Program
    {
        static void Main(string[] args)
        {
            var instance = new ServiceInstance.ServiceInstance();
            instance.Run();

            Console.ReadLine();
        }
    }
}
