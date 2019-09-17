using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Dtos;

namespace FPLModeling
{
    public class ModelCalculator : IModelCalculator
    {
        public double PREVIOUS_SEASON_WEIGHT;
        public double PREVIOUS_SEASON_DIFFERENT_TEAM_WEIGHT;

        public int POINTS_GOALS_DEFENDER;
        public int POINTS_CLEAN_SHEET_DEFENDER;

        public int POINTS_GOALS_GOALKEEPER;
        public int POINTS_CLEAN_SHEET_GOALKEEPER;

        public int POINTS_GOALS_MIDFIELDER;
        public int POINTS_CLEAN_SHEET_MIDFIELDER;

        public int POINTS_GOALS_FORWARD;
        public int POINTS_CLEAN_SHEET_FORWARD;

        public BaseResultDto<List<CalculatedPlayerStatisticsDto>> Calculate(List<PlayerSeasonStatisticsDto> players)
        {
            SetUpParameters();

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

            var regModel = Utility.GetLinearRegressionModel(players.Select(p => (double)p.SeasonTeam.XGAgainst / p.SeasonTeam.GamesPlayed), players.Select(p => (double)p.SeasonTeam.CleanSheets / p.SeasonTeam.GamesPlayed));

            foreach (var player in listOfPlayers)
            {

                var pssList = players.Where(p => p.Player.Name.Equals(player));

                if (player == "John Stones")
                {
                    var t = 1;
                }

                var totalMinutes = 0;
                Double totalXG90 = 0.0;
                Double totalXA90 = 0.0;
                Double totalYC90 = 0.0;
                Double totalCS90 = 0.0;
                var minutesPlayedCurrentYear = 0;
                var totalYC = 0;
                var totalApps = 0;

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
                    var team = pss.SeasonTeam.Team.Name;
                    
                    if (pss.SeasonTeam.Season.StartYear == currentStartYear)
                    {
                        totalMinutes += pss.MinutesPlayed;
                        xg90 = pss.XG90 * pss.MinutesPlayed;
                        xa90 = pss.XA90 * pss.MinutesPlayed;
                        minutesPlayedCurrentYear = pss.MinutesPlayed;
                        
                        if (pss.Apps > 0)
                        {
                            totalCS90 = regModel.Alpha + (currentSeasonPlayer.SeasonTeam.XGAgainst / currentSeasonPlayer.SeasonTeam.GamesPlayed * regModel.Beta);
                            totalCS90 = (double)totalCS90 * ((double)pss.Apps / pss.SeasonTeam.GamesPlayed);
                        }
                    }
                    else
                    {
                        var fraction = Math.Max(0, Math.Pow(PREVIOUS_SEASON_WEIGHT, (currentStartYear - pss.SeasonTeam.Season.StartYear)));
                        fraction *= team.Equals(currentTeam) ? 1:PREVIOUS_SEASON_DIFFERENT_TEAM_WEIGHT;

                        totalMinutes += (int) Math.Round(pss.MinutesPlayed * fraction);
                        xg90 = pss.XG90 * pss.MinutesPlayed * fraction;
                        xa90 = pss.XA90 * pss.MinutesPlayed * fraction;

                        

                        if (currentStartYear - pss.SeasonTeam.Season.StartYear == 1)
                        {
                            if (!pss.SeasonTeam.Team.Name.Equals(currentTeam))
                            {
                                fraction = 0;
                            }
                        }
                    }

                    totalXG90 += xg90;
                    totalXA90 += xa90;
                    totalYC += pss.YellowCards;
                    totalApps += pss.Apps;

                    
                }

                if (totalApps > 0)
                {
                    totalYC90 += (double)totalYC / totalApps;
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
                    xPTotal = GetXpTotal(position, cost, totalXG90, totalXA90, totalCS90, totalYC90),
                    xYc = totalYC90,
                 });
            }

            result.DataObject = resultList;
            return result;
        }

        private double GetXPPoundMinPlayed(Constants.PositionEnum position, double lastCost, double totalXG90, double totalXA90, double totalCS90, double totalYC90, int minPlayed)
        {
            return GetXPPound90(position, lastCost, totalXG90, totalXA90, totalCS90, totalYC90) * (double)(minPlayed/90);
        }

        private double GetXpTotal(Constants.PositionEnum position, double lastCost, double totalXG90, double totalXA90, double totalCS90, double totalYC90)
        {
            return GetXPPound90(position, lastCost, totalXG90, totalXA90, totalCS90, totalYC90) * lastCost;
        }

        private double GetXPPound90(Constants.PositionEnum position, double lastCost, double totalXG90, double totalXA90, double totalCS90, double totalYC90)
        {
            var xp = 0.0;
            int pGoal;
            int pCS;

            switch (position)
            {
                case Constants.PositionEnum.Defender:
                    pGoal = POINTS_GOALS_DEFENDER;
                    pCS = POINTS_CLEAN_SHEET_DEFENDER;
                    break;
                case Constants.PositionEnum.Midfielder:
                    pGoal = POINTS_GOALS_MIDFIELDER;
                    pCS = POINTS_CLEAN_SHEET_MIDFIELDER;
                    break;
                case Constants.PositionEnum.GoalKeeper:
                    pGoal = POINTS_GOALS_GOALKEEPER;
                    pCS = POINTS_CLEAN_SHEET_GOALKEEPER;
                    break;
                case Constants.PositionEnum.Forward:
                    pGoal = POINTS_GOALS_FORWARD;
                    pCS = POINTS_CLEAN_SHEET_FORWARD;
                    break;
                default:
                    throw new Exception();
            }

            if (lastCost == 0)
            {
                throw new Exception();
            }

            xp = (totalCS90 * pCS + totalXA90 * 3 + totalXG90 * pGoal - totalYC90) / lastCost;

            return xp;
        }

        private void SetUpParameters()
        {
            PREVIOUS_SEASON_WEIGHT = Utility.GetApplicationSetting<double>("PrevSeasonWeight");
            PREVIOUS_SEASON_DIFFERENT_TEAM_WEIGHT = Utility.GetApplicationSetting<double>("PrevSeasonDiffTeamWeight");

            POINTS_GOALS_DEFENDER = Utility.GetApplicationSetting<int>("PointsGoalsDefender");
            POINTS_CLEAN_SHEET_DEFENDER = Utility.GetApplicationSetting<int>("PointsCleanSheetDefender");

            POINTS_GOALS_GOALKEEPER = Utility.GetApplicationSetting<int>("PointsGoalsGoalkeeper");
            POINTS_CLEAN_SHEET_GOALKEEPER = Utility.GetApplicationSetting<int>("PointsCleanSheetGoalKeeper");

            POINTS_GOALS_MIDFIELDER = Utility.GetApplicationSetting<int>("PointsGoalsMidfielder");
            POINTS_CLEAN_SHEET_MIDFIELDER = Utility.GetApplicationSetting<int>("PointsCleanSheetMidfielder");

            POINTS_GOALS_FORWARD = Utility.GetApplicationSetting<int>("PointsGoalsForward");
            POINTS_CLEAN_SHEET_FORWARD = Utility.GetApplicationSetting<int>("PointsCleanSheetForward");
        }
    }
}
