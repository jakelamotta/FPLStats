namespace Common.Dtos
{
    public class SeasonDto
    {
        public int Id { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public bool IsCurrent { get; set; }
        public bool DataComplete { get; set; }
        public LeagueDto League { get; set; }
    }
}
