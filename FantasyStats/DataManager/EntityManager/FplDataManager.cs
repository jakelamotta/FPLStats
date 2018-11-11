using Common.Dtos;
using Common.Logging;
using DataManager.AutoMapper;
using DataManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DataManager.EntityManager
{
    public class FplDataManager: DataManagerBase, IFplDataManager
    {
        private readonly IContextFactory _contextFactory;
        private readonly ICustomLogger _logger;

        public FplDataManager(IContextFactory contextFactory, ILogFactory logFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            _logger = logFactory.Create();
            _contextFactory = contextFactory;
        }

        #region Writes
        public BaseResultDto<int> SavePlayerStatisticsList(List<PlayerSeasonStatisticsDto> players)
        {
            var result = new BaseResultDto<int>();

            using (var context = _contextFactory.Create())
            {

                var allSeasons = context.Seasons.ToList();

                foreach (var playerDto in players)
                {
                    var dbPlayer = context.PlayerSeasonStatistics.FirstOrDefault(p => p.Player.Name == playerDto.Player.Name
                            && p.SeasonTeam.Season.StartYear == playerDto.SeasonTeam.Season.StartYear);

                    if (dbPlayer != null)
                    {
                        var tempPlayer = context.Players.FirstOrDefault(p => p.Name == playerDto.Player.Name);

                        if (tempPlayer.LastCost != playerDto.Player.LastCost && tempPlayer.LastCost > 0)
                        {
                            var costChange = new PlayerCostChange
                            {
                                ChangeCaptureDate = DateTime.Today,
                                PlayerId = tempPlayer.Id,
                                Delta = playerDto.Player.LastCost - tempPlayer.LastCost
                            };

                            context.PlayerCostChanges.Add(costChange);
                            context.SaveChanges();
                        }

                        dbPlayer.MinutesPlayed = playerDto.MinutesPlayed;
                        dbPlayer.OwnGoals = playerDto.OwnGoals;
                        dbPlayer.PenaltiesMissed = playerDto.PenaltiesMissed;
                        dbPlayer.XA = playerDto.XA;
                        dbPlayer.XA90 = playerDto.XA90;
                        dbPlayer.XG = playerDto.XG;
                        dbPlayer.XG90 = playerDto.XG90;
                        dbPlayer.YellowCards = playerDto.YellowCards;
                        dbPlayer.RedCards = playerDto.RedCards;
                        dbPlayer.Apps = playerDto.Apps;
                        dbPlayer.CleanSheets = playerDto.CleanSheets;
                        tempPlayer.LastCost = playerDto.Player.LastCost;

                        context.Players.AddOrUpdate(tempPlayer);
                        context.PlayerSeasonStatistics.AddOrUpdate(dbPlayer);
                        context.SaveChanges();
                    }
                    else
                    {
                        var seasonTeam = context.SeasonTeams.FirstOrDefault(st => st.Season.StartYear == playerDto.SeasonTeam.Season.StartYear
                            && st.Season.League.Country == playerDto.SeasonTeam.Season.League.Country
                            && st.Team.Name.Equals(playerDto.SeasonTeam.Team.Name));

                        if (seasonTeam == null)
                        {
                            var season = allSeasons.FirstOrDefault(s => s.StartYear == playerDto.SeasonTeam.Season.StartYear);

                            if (season == null)
                            {
                                result.Status = false;
                                return result;
                            }

                            var dbTeam = context.Teams.FirstOrDefault(t => t.Name.Equals(playerDto.SeasonTeam.Team.Name));

                            if (dbTeam == null)
                            {
                                dbTeam = new Team
                                {
                                    Name = playerDto.SeasonTeam.Team.Name
                                };

                                context.Teams.Add(dbTeam);
                                context.SaveChanges();
                            }

                            seasonTeam = new SeasonTeam
                            {
                                TeamId = dbTeam.Id,   
                                SeasonId = season.Id
                            };

                            context.SeasonTeams.Add(seasonTeam);
                            context.SaveChanges();
                        }

                        var p = context.Players.FirstOrDefault(pa => pa.Name == playerDto.Player.Name);

                        if (p == null)
                        {
                            var position = context.Positions.FirstOrDefault(po => po.AppId == (int)playerDto.Player.Position);

                            p = new Player
                            {
                                ExternalId = playerDto.Player.ExternalId,
                                Name = playerDto.Player.Name,
                                SecondName = playerDto.Player.SecondName,
                                PositionId = position.id,
                                LastCost = playerDto.Player.LastCost
                            };

                            context.Players.Add(p);
                            context.SaveChanges();
                        }

                        

                        dbPlayer = new PlayerSeasonStatistics
                        {
                            PlayerId = p.Id,
                            SeasonTeamId = seasonTeam.Id,
                            MinutesPlayed = playerDto.MinutesPlayed,
                            OwnGoals = playerDto.OwnGoals,
                            PenaltiesMissed = playerDto.PenaltiesMissed,
                            RedCards = playerDto.RedCards,
                            XA = playerDto.XA,
                            XA90 = playerDto.XA90,
                            XG = playerDto.XG,
                            XG90 = playerDto.XG90,
                            YellowCards = playerDto.YellowCards,
                            Apps = playerDto.Apps,
                            CleanSheets = playerDto.CleanSheets
                        };

                        context.PlayerSeasonStatistics.Add(dbPlayer);
                        context.SaveChanges();
                    }
                }

                
            }            

            return result;
        }

        public BaseResultDto<int> SaveCalculatedPlayerStatistics(List<CalculatedPlayerStatisticsDto> players)
        {
            var result = new BaseResultDto<int>();
            result.Status = true;

            using (var context = _contextFactory.Create())
            {
                context.CalculatedPlayerStatistics.RemoveRange(context.CalculatedPlayerStatistics);
                context.SaveChanges();

                foreach (var player in players)
                {
                    var dbPlayer = new CalculatedPlayerStatistics
                    {
                        MinutesPlayed = player.MinutesPlayed,
                        Name = player.Name,
                        xPAbs = player.xPAbs,
                        xPPound90 = player.xPPound90,
                        xPPoundMinPlayed = player.xPPoundMinPlayed,
                        xRc = player.xRc,
                        xYc = player.xYc
                    };

                    context.CalculatedPlayerStatistics.Add(dbPlayer);

                    try
                    {
                        context.SaveChanges();
                    }
                    catch(Exception e)
                    {
                        var t = 1;
                    }
                }

                
            }

            return result;
        }

        #endregion

        #region Reads

        public BaseResultDto<List<int>> GetNonCompleteYears()
        {
            var result = new BaseResultDto<List<int>>();
            result.Status = true;

            var years = new List<int>();

            using (var context = _contextFactory.Create())
            {
                years = context.Seasons.Where(s => !s.DataComplete).Select(s => s.StartYear).ToList();
            }

            result.DataObject = years;

            return result;
        }

        public List<PlayerSeasonStatisticsDto> GetAllPlayerStatistics()
        {
            using (var context = _contextFactory.Create())
            {
                return context.PlayerSeasonStatistics
                    .Include("SeasonTeam")
                    .Include("SeasonTeam.Team")
                    .Include("SeasonTeam.Season")
                    .Include("Player")
                    .Include("Player.Position")
                    .ToDtoList<PlayerSeasonStatisticsDto>(Mapper).ToList();
            }
        }
        #endregion
    }
}
