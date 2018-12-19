namespace Common.Dtos
{
    public class SeasonTeamDto
    {
        public int Id { get; set; }
        public SeasonDto Season { get; set; }
        public TeamDto Team { get; set; }
        public int Position { get; set; }
        public int GamesPlayed { get; set; }
        public int Won { get; set; }
        public int Drawn { get; set; }
        public int Lost { get; set; }
        public int Points { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int CleanSheets { get; set; }
        public double XGFor { get; set; }
        public double XGAgainst { get; set; }
        public double XPoints { get; set; }
    }
}
