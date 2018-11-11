using Common.Logging;
using Data.Providers;
using DataManager;
using DataManager.EntityManager;
using FPLModeling;
using Unity;

namespace ServiceInstance
{
    public class Bootstrapper
    {
        public static UnityContainer CreateUnityContainer()
        {
            var con = new UnityContainer();

            con.RegisterType<IStatsProvider, StatsProvider>();
            con.RegisterType<IFplDataManager, FplDataManager>();
            con.RegisterType<ILogFactory, LogFactory>();
            con.RegisterType<IFplStats, IFplStats>();
            con.RegisterType<IContextFactory, FPLStatsContextFactory>();
            con.RegisterType<IModelCalculator, ModelCalculator>();

            return con;
        }
    }
}
