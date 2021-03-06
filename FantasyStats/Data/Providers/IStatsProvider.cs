﻿using Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Providers
{
    public interface IStatsProvider
    {
        BaseResultDto<List<PlayerSeasonStatisticsDto>> GetPlayers(IEnumerable<SeasonDto> seasons);
        BaseResultDto<List<SeasonTeamDto>> GetTeams(IEnumerable<SeasonDto> seasons);
    }
}