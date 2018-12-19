namespace Common.Dtos
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    public class MatchDto
    {
        public int Id { get; set; }

        public SeasonTeamDto HomeTeam { get; set; }
        public SeasonTeamDto AwayTeam { get; set; }

        public int HomeGoals { get; set; }

        public double HomeXGoals { get; set; }
        public double HomeXGoalsAgainst { get; set; }

        public int AwayGoals { get; set; }

        public double AwayXGoals { get; set; }
        public double AwayXGoalsAgainst { get; set; }
    }
}
