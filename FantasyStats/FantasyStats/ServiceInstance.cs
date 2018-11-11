using Data.Providers;
using DataManager.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyStats
{
    public class ServiceInstance: IServiceInstance
    {
        private readonly IStatsProvider _statsProvider;
        private readonly IFplDataManager _fplDataManager;


        public ServiceInstance(IStatsProvider statsProvider, IFplDataManager fplDataManager)
        {
            _statsProvider = statsProvider;
            _fplDataManager = fplDataManager;
        }

        public void work()
        {
            var result = _statsProvider.GetPlayers(new List<int> { 2015, 2016, 2017, 2018 });

            if (result.Status)
            {
                _fplDataManager.SavePlayerStatisticsList(result.DataObject);
            }
        }
    }
}
