using Common.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.EntityManager
{
    public interface IFplDataManager
    {
        #region Writes
        BaseResultDto<int> SavePlayerStatisticsList(List<PlayerSeasonStatisticsDto> players);
        #endregion

        #region Reads
        #endregion
    }
}
