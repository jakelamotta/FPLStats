namespace Common.Dtos
{
    public class PlayerSeasonStatisticsDto
    {
        public PlayerDto Player { get; set; }
        public SeasonTeamDto SeasonTeam {get;set;}
        public int Id { get; set; }

        public int BonusPointSystem { get; set; }
        public int CleanSheets { get; set; }
        public int OwnGoals { get; set; }
        public int PenaltiesMissed { get; set; }
        public int RedCards { get; set; }
        public int YellowCards { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public double XG { get; set; }
        public double XA { get; set; }
        public double XG90 { get; set; }
        public double XA90 { get; set; }
        public Constants.Position Position { get; set; }
        public int MinutesPlayed { get; set; }
        public int Apps { get; set; }
    }
}
