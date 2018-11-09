using Data;
using Data.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStats
{
    class Program
    {
        static void Main(string[] args)
        {
            //var sp = new StatsProvider();

            ScrapingUtility.ExecuteScrapingUtility(2015);

            //sp.GetPlayer(1);
            //Console.ReadLine();
        }
    }
}
