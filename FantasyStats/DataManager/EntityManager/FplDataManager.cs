using Common.Dtos;
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

        public FplDataManager(IContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException(nameof(contextFactory));
            }

            _contextFactory = contextFactory;
        }

        #region Writes
        public BaseResultDto<int> SavePlayerStatisticsList(List<PlayerSeasonStatisticsDto> players)
        {
            var result = new BaseResultDto<int>();

            using (var context = _contextFactory.Create()) {

                var allSeasons = context.Seasons.ToList();

                foreach (var playerDto in players)
                {
                    var dbPlayer = context.PlayerSeasonStatistics.FirstOrDefault(p => p.Player.Name.Equals(playerDto.Player.Name)
                        && p.SeasonTeam.Season.StartYear == playerDto.SeasonTeam.Season.StartYear
                        && p.SeasonTeam.Season.League.Country == playerDto.SeasonTeam.Season.League.Country);

                    if (dbPlayer != null)
                    {
                        if (dbPlayer.Player.LastCost != playerDto.Player.LastCost)
                        {
                            var costChange = new PlayerCostChange
                            {
                                ChangeCaptureDate = DateTime.Today,
                                PlayerId = dbPlayer.Id,
                                Delta = playerDto.Player.LastCost - dbPlayer.Player.LastCost
                            };

                            context.PlayerCostChanges.Add(costChange);
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
                        dbPlayer.Player.LastCost = playerDto.Player.LastCost;

                        context.PlayerSeasonStatistics.AddOrUpdate(dbPlayer);
                    }
                    else
                    {
                        var seasonTeam = context.SeasonTeams.FirstOrDefault(st => st.Season.StartYear == playerDto.SeasonTeam.Season.StartYear
                            && st.Season.League.Country == playerDto.SeasonTeam.Season.League.Country);

                        if (seasonTeam == null)
                        {
                            var season = allSeasons.FirstOrDefault(s => s.StartYear == playerDto.SeasonTeam.Season.StartYear);

                            if (season == null)
                            {
                                result.Status = false;
                                return result;
                            }

                            seasonTeam = new SeasonTeam
                            {
                                Team = new Team
                                {
                                    Name = playerDto.SeasonTeam.Team.Name
                                },
                                SeasonId = season.Id
                            };

                            context.SeasonTeams.Add(seasonTeam);
                            context.SaveChanges();
                        }

                        var p = context.Players.FirstOrDefault(pa => pa.ExternalId == playerDto.Player.ExternalId);

                        if (p == null)
                        {
                            p = new Player
                            {
                                ExternalId = playerDto.Player.ExternalId,
                                Name = playerDto.Player.Name,
                                SecondName = playerDto.Player.SecondName
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
                        };

                        context.PlayerSeasonStatistics.Add(dbPlayer);
                    }
                }

                context.SaveChanges();
            }            

            return result;
        }
        #endregion

        #region Reads
        #endregion
    }
}
