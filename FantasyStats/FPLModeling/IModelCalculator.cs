using Common.Dtos;
using System.Collections.Generic;

namespace FPLModeling
{
    public interface IModelCalculator
    {
        BaseResultDto<List<CalculatedPlayerStatisticsDto>> Calculate(List<PlayerSeasonStatisticsDto> players);
    }
}
