using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Dtos;
using Common.Logging;

namespace Data.Providers
{
    public class StatsProvider : IStatsProvider
    {
        private readonly ICustomLogger _logger;

        public StatsProvider(ILogFactory logFactory)
        {
            _logger = logFactory.Create();
        }

        public BaseResultDto<List<PlayerSeasonStatisticsDto>> GetPlayers(IEnumerable<SeasonDto> seasons)
        {
            var result = new BaseResultDto<List<PlayerSeasonStatisticsDto>>();
            var players = new List<PlayerSeasonStatisticsDto>();
            result.Status = true;
            var dataPath = Utility.GetApplicationSetting<string>("PathToDataFiles");

            foreach (var season in seasons)
            {
                var pythonResult =  PythonUtility.ExecutePythonScript(new PythonRequestDto
                {
                    Command = Utility.GetApplicationSetting<string>("PlayerScript"),
                    Params = new string[] { season.StartYear.ToString(), season.League.Name }
                });

                if (pythonResult.Status)
                {
                    var fname =  result.DataObject;

                    var data = Utility.GetFileData(dataPath + pythonResult.Output);


                    foreach (var line in data.Skip(1))
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
                        int apps = 0;
                        Constants.PositionEnum pos;

                        double xa = 0.0;
                        double xa90 = 0.0;
                        double xg = 0.0;
                        double xg90 = 0.0;
                        double cost = 0.0;

                        var parseStatus = int.TryParse(lineAsList[0], out externalId);
                        parseStatus &= int.TryParse(lineAsList[1], out assists);
                        parseStatus &= int.TryParse(lineAsList[2], out bps);
                        parseStatus &= int.TryParse(lineAsList[3], out cs);
                        parseStatus &= Enum.TryParse(lineAsList[4], out pos);
                        parseStatus &= int.TryParse(lineAsList[5], out goals);
                        parseStatus &= double.TryParse(lineAsList[7].Replace(".", ","), out cost);
                        parseStatus &= int.TryParse(lineAsList[8], out og);
                        parseStatus &= int.TryParse(lineAsList[9], out pm);
                        parseStatus &= int.TryParse(lineAsList[10], out rc);
                        parseStatus &= int.TryParse(lineAsList[11], out yc);
                        parseStatus &= int.TryParse(lineAsList[15], out apps);
                        parseStatus &= int.TryParse(lineAsList[16], out min);

                        parseStatus &= double.TryParse(lineAsList[20].Replace(".",","), out xa);
                        parseStatus &= double.TryParse(lineAsList[22].Replace(".", ","), out xa90);
                        parseStatus &= double.TryParse(lineAsList[19].Replace(".", ","), out xg);
                        parseStatus &= double.TryParse(lineAsList[21].Replace(".", ","), out xg90);
                        cost /= 10;

                        if (parseStatus)
                        {
                            var playerStatisticsDto = new PlayerSeasonStatisticsDto
                            {
                                Player = new PlayerDto
                                {
                                    ExternalId = externalId,
                                    Name = lineAsList[12],
                                    SecondName = "",
                                    LastCost = cost,
                                    Position = pos
                                },
                                SeasonTeam = new SeasonTeamDto
                                {
                                    Season = season,
                                    Team = new TeamDto
                                    {
                                        Name = ConvertDoubleTeam(lineAsList[14])
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
                                MinutesPlayed = min,
                                Apps = 0
                            };

                            players.Add(playerStatisticsDto);
                        }
                        else
                        {
                            _logger.Info($"Couldnt parse line: {line}");
                        }
                    }
                }
            }

            result.DataObject = players;

            return result;
        }

        public BaseResultDto<List<SeasonTeamDto>> GetTeams(IEnumerable<SeasonDto> seasons)
        {
            var result = new BaseResultDto<List<SeasonTeamDto>>();
            result.DataObject = new List<SeasonTeamDto>();
            var dataPath = Utility.GetApplicationSetting<string>("PathToDataFiles");

            foreach (var season in seasons)
            {
                var pythonRequest = new PythonRequestDto
                {
                    Command = Utility.GetApplicationSetting<string>("TeamScript"),
                    Params = new string[] { season.StartYear.ToString(), season.League.Name }
                };
               

                var pythonResult = PythonUtility.ExecutePythonScript(pythonRequest);

                if (pythonResult.Status)
                {
                    var cleanSheetsFile = Utility.GetFileData(dataPath + "team_Fpl.csv");
                    
                    foreach (var line in Utility.GetFileData(dataPath + pythonResult.Output).Skip(1))
                    {
                        var lineAsList = Utility.CsvRowToList(line);

                        int position;
                        int matches;
                        int won;
                        int drawn;
                        int lost;
                        int goals;
                        int goalsAgainst;
                        int points;
                        double xG;
                        double xGAgainst;
                        double xPoints;

                        var status = int.TryParse(lineAsList[0], out position);
                        status &= int.TryParse(lineAsList[2], out matches);
                        status &= int.TryParse(lineAsList[3], out won);
                        status &= int.TryParse(lineAsList[4], out drawn);
                        status &= int.TryParse(lineAsList[5], out lost);
                        status &= int.TryParse(lineAsList[6], out goals);
                        status &= int.TryParse(lineAsList[7], out goalsAgainst);
                        status &= int.TryParse(lineAsList[8], out points);

                        status &= double.TryParse(lineAsList[9].StripXValues().Replace(".",","), out xG);
                        status &= double.TryParse(lineAsList[10].StripXValues().Replace(".", ","), out xGAgainst);
                        status &= double.TryParse(lineAsList[11].StripXValues().Replace(".", ","), out xPoints);

                        if (status)
                        {
                            var seasonTeam = new SeasonTeamDto
                            {
                                Season = season,
                                Team = new TeamDto
                                {
                                    Name = ConvertDoubleTeam(lineAsList[1])
                                },
                                Drawn = drawn,
                                GoalsAgainst = goalsAgainst,
                                GoalsFor = goals,
                                Lost = lost,
                                GamesPlayed = matches,
                                Points = points,
                                Position = position,
                                Won = won,
                                XGAgainst = xGAgainst,
                                XGFor = xG,
                                XPoints = xPoints
                            };

                            result.DataObject.Add(seasonTeam);
                        }
                    }

                    //foreach (var line in cleanSheetsFile)
                    //{
                    //    var csLineAsList = Utility.CsvRowToList(line);
                    //    int cs;

                    //    var st = result.DataObject.FirstOrDefault(r => r.Team.Name.Equals(csLineAsList[1])
                    //        && r.Season.Id == season.Id);

                    //    var status = int.TryParse(csLineAsList[2], out cs);

                    //    if (!status || st == null)
                    //    {
                    //        throw new Exception();
                    //    }

                    //    st.CleanSheets = cs;
                    //}

                }
                else
                {
                    var e = new Exception(pythonResult.ErrorMessage);
                    _logger.Error(e, "Could not parse team data");
                    throw e;
                }
            }

            result.Status = result.DataObject.Count/seasons.Count() == 20;

            return result;
        }

        private string ConvertDoubleTeam(string v)
        {
            if (!v.Contains("-"))
            {
                return v;
            }

            var teams = v.Split(new string[] { "--" }, StringSplitOptions.None);

            if (teams.Count() > 1)
            {
                return teams[0];
            }

            throw new Exception();
        }
    }
}
