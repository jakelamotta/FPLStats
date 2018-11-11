using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Dtos;

namespace Data.Providers
{
    public class StatsProvider : IStatsProvider
    {
        private string _dataFileName = "D:\\repos\\FPLStats\\FPLStats\\FantasyStats\\Data\\filedata\\";

        public BaseResultDto<List<PlayerSeasonStatisticsDto>> GetPlayers(IEnumerable<int> years)
        {
            var result = new BaseResultDto<List<PlayerSeasonStatisticsDto>>();
            var players = new List<PlayerSeasonStatisticsDto>();
            result.Status = true;

            foreach (var year in years)
            {
                var status = true;// ScrapingUtility.ExecuteScrapingUtility(year);
                var season = new SeasonDto
                {
                    StartYear = year
                };

                if (status)
                {
                    _dataFileName += $"attackingreturn_{year}.csv";

                    var data = Utility.GetFileData(_dataFileName);


                    foreach (var line in data)
                    {
                        var lineAsList = Utility.CsvRowToList(line);


                        int externalId = 0;
                        int assists = 0;
                        int bps = 0;
                        int cs = 0;
                        int goals = 0;
                        int og = 0;
                        int pm = 0;
                        int rc = 0;
                        int yc = 0;
                        int min = 0;

                        double xa = 0.0;
                        double xa90 = 0.0;
                        double xg = 0.0;
                        double xg90 = 0.0;
                        double cost = 0.0;

                        var parseStatus = int.TryParse(lineAsList[0], out externalId);
                        parseStatus &= int.TryParse(lineAsList[21], out assists);
                        parseStatus &= int.TryParse(lineAsList[1], out bps);
                        parseStatus &= int.TryParse(lineAsList[2], out cs);
                        parseStatus &= int.TryParse(lineAsList[20], out goals);
                        parseStatus &= int.TryParse(lineAsList[6], out og);
                        parseStatus &= int.TryParse(lineAsList[7], out pm);
                        parseStatus &= int.TryParse(lineAsList[10], out rc);
                        parseStatus &= int.TryParse(lineAsList[14], out yc);
                        parseStatus &= int.TryParse(lineAsList[19], out min);

                        parseStatus &= double.TryParse(lineAsList[23].Replace(".",","), out xa);
                        parseStatus &= double.TryParse(lineAsList[25].Replace(".", ","), out xa90);
                        parseStatus &= double.TryParse(lineAsList[22].Replace(".", ","), out xg);
                        parseStatus &= double.TryParse(lineAsList[24].Replace(".", ","), out xg90);
                        parseStatus &= double.TryParse(lineAsList[5].Replace(".", ","), out cost);
                        cost /= 10;

                        if (parseStatus)
                        {
                            var playerStatisticsDto = new PlayerSeasonStatisticsDto
                            {
                                Player = new PlayerDto
                                {
                                    ExternalId = externalId,
                                    Name = lineAsList[15],
                                    SecondName = lineAsList[11],
                                    LastCost = cost
                                },
                                SeasonTeam = new SeasonTeamDto
                                {
                                    Season = season,
                                    Team = new TeamDto
                                    {
                                        Name = lineAsList[17]
                                    }
                                },
                                Assists = assists,
                                BonusPointSystem = bps,
                                CleanSheets = cs,
                                Goals = goals,
                                OwnGoals = og,
                                PenaltiesMissed = pm,
                                RedCards = rc,
                                XA = xa,
                                XA90 = xa90,
                                XG = xg,
                                XG90 = xg90,
                                YellowCards = yc,
                                MinutesPlayed = min
                            };

                            players.Add(playerStatisticsDto);
                        }
                    }
                }
            }

            return result;
        }
    }
}
