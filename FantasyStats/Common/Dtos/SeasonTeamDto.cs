namespace Common.Dtos
{
    public class SeasonTeamDto
    {
        public int Id { get; set; }
        public SeasonDto Season { get; set; }
        public TeamDto Team { get; set; }
        public int? Position { get; set; }
        public int? MatchesPlayed { get; set; }
        public int? Won { get; set; }
        public int? Draws { get; set; }
        public int? Losses { get; set; }
        public int? Points { get; set; }
        public int? GoalsFor { get; set; }
        public int? GoalsAgainst { get; set; }
        public int? GoalDiff { get; set; }
        public double XGFor { get; set; }
        public double XGA { get; set; }
    }
}
