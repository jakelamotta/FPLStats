using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Dtos;

namespace Data.Providers
{
    public class StatsProvider : IStatsProvider
    {
        private readonly string _underStatUrl = "https://understat.com/league/EPL/2018";

        public BaseResultDto<PlayerDto> GetPlayer(int id)
        {
            var player = new BaseResultDto<PlayerDto>();

            ScrapingUtility.ExecuteScrapingUtility(2018);

            return player;
        }
    }
}
