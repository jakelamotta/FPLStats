using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Dtos;

namespace FPLModeling
{
    public class ModelCalculator : IModelCalculator
    {
        public ModelCalculator()
        {

        }

        public BaseResultDto<List<CalculatedPlayerStatisticsDto>> Calculate(List<PlayerSeasonStatisticsDto> players)
        {
            var result = new BaseResultDto<List<CalculatedPlayerStatisticsDto>>();
            result.Status = true;

            var resultList = new List<CalculatedPlayerStatisticsDto>();
            var today = DateTime.Today;
            int currentStartYear;

            if (today.Month > 6)
            {
                currentStartYear = today.Year;
            }
            else
            {
                currentStartYear = today.Year - 1;
            }

            var listOfPlayers = players.Select(p => p.Player.Name).Distinct().ToList();

            foreach (var player in listOfPlayers)
            {
                var pssList = players.Where(p => p.Player.Name.Equals(player));

                var totalMinutes = 0;
                Double totalXG90 = 0.0;
                Double totalXA90 = 0.0;
                Double totalYC90 = 0.0;
                Double totalCS90 = 0.0;
                var minutesPlayedCurrentYear = 0;

                var currentSeasonPlayer = pssList.FirstOrDefault(pss => pss.SeasonTeam.Season.StartYear == currentStartYear);
                var currentTeam = "";
                if (currentSeasonPlayer != null)
                {
                    currentTeam = currentSeasonPlayer.SeasonTeam.Team.Name;
                }

                var position = pssList.First().Player.Position;
                var cost = pssList.First().Player.LastCost;

                foreach (var pss in pssList)
                {
                    var xg90 = 0.0;
                    var xa90 = 0.0;
                    var xyc90 = 0.0;
                    var xcs90 = 0.0;

                    if (pss.SeasonTeam.Season.StartYear == currentStartYear)
                    {
                        totalMinutes += pss.MinutesPlayed;
                        xg90 = pss.XG90 * pss.MinutesPlayed;
                        xa90 = pss.XA90 * pss.MinutesPlayed;
                        minutesPlayedCurrentYear = pss.MinutesPlayed;
                        
                        if (pss.Apps > 0)
                        {
                            totalCS90 += (pss.CleanSheets * 10 / pss.Apps);
                        }
                    }
                    else
                    {
                        var fraction = Math.Max(0, 1 - ((currentStartYear - pss.SeasonTeam.Season.StartYear) * .37));
                        fraction *= pss.SeasonTeam.Team.Name.Equals(currentTeam) ? 1:0.6;

                        totalMinutes += (int) Math.Round(pss.MinutesPlayed * fraction);
                        xg90 = pss.XG90 * pss.MinutesPlayed * fraction;
                        xa90 = pss.XA90 * pss.MinutesPlayed * fraction;

                        if (currentStartYear - pss.SeasonTeam.Season.StartYear == 1)
                        {
                            if (!pss.SeasonTeam.Team.Name.Equals(currentTeam))
                            {
                                fraction = 0;
                            }

                            if (pss.Apps > 0)
                            {
                                totalCS90 +=  (pss.CleanSheets * 10/ pss.Apps) * fraction;
                            }
                        }
                    }

                    totalXG90 += xg90;
                    totalXA90 += xa90;
                    if (pss.Apps > 0)
                    {
                        totalYC90 += pss.YellowCards / pss.Apps;
                    }
                }

                if (totalMinutes > 0)
                {
                    totalXG90 /= totalMinutes;
                    totalXA90 /= totalMinutes;
                }

                resultList.Add(new CalculatedPlayerStatisticsDto
                {
                    MinutesPlayed = minutesPlayedCurrentYear,
                    Name = player,
                    xPPound90 = GetXPPound90(position, cost, totalXG90, totalXA90, totalCS90, totalYC90),
                    xPPoundMinPlayed = GetXPPoundMinPlayed(position, cost, totalXG90, totalXA90, totalCS90, totalYC90, minutesPlayedCurrentYear),
                    xYc = totalYC90,
                 });
            }

            result.DataObject = resultList;
            return result;
        }

        private double GetXPPoundMinPlayed(Constants.PositionEnum position, double lastCost, double totalXG90, double totalXA90, double totalCS90, double totalYC90, int minPlayed)
        {
            return GetXPPound90(position, lastCost, totalXG90, totalXA90, totalCS90, totalYC90) * (minPlayed/90);
        }

        private double GetXPPound90(Constants.PositionEnum position, double lastCost, double totalXG90, double totalXA90, double totalCS90, double totalYC90)
        {
            var xp = 0.0;
            int pGoal;
            int pCS;

            switch (position)
            {
                case Constants.PositionEnum.Defender:
                    pGoal = 6;
                    pCS = 4;
                    break;
                case Constants.PositionEnum.Midfielder:
                    pGoal = 5;
                    pCS = 1;
                    break;
                case Constants.PositionEnum.GoalKeeper:
                    pGoal = 6;
                    pCS = 4;
                    break;
                case Constants.PositionEnum.Forward:
                    pGoal = 4;
                    pCS = 0;
                    break;
                default:
                    throw new Exception();
            }

            if (lastCost == 0)
            {
                throw new Exception();
            }

            xp = ((totalCS90 * pCS)/10 + totalXA90 * 3 + totalXG90 * pGoal - totalYC90) / lastCost;

            return xp;
        }
    }
}
