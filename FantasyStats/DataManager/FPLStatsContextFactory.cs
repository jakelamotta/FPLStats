using DataManager.Models;

namespace DataManager
{
    public class FPLStatsContextFactory : IContextFactory
    {
        public FPLStatsContext Create()
        {
            return new FPLStatsContext();
        }
    }
    
}
