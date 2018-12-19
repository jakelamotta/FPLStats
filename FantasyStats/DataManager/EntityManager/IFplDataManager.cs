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
        BaseResultDto<int> SaveTeamStatisticsList(List<SeasonTeamDto> teams);
        BaseResultDto<int> SaveCalculatedPlayerStatistics(List<CalculatedPlayerStatisticsDto> players);
        #endregion

        #region Reads
        BaseResultDto<List<SeasonDto>> GetNonCompleteSeasons();
        List<PlayerSeasonStatisticsDto> GetAllPlayerStatistics();
        #endregion
    }
}
