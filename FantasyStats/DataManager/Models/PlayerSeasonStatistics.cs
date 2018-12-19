using Common.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.Models
{
    public class PlayerSeasonStatistics
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("SeasonTeam")]
        public int SeasonTeamId { get; set; }
        public virtual SeasonTeam SeasonTeam { get; set; }

        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; }

        public int BonusPointSystem { get; set; }
        public int CleanSheets { get; set; }
        public int OwnGoals { get; set; }
        public int PenaltiesMissed { get; set; }
        public int RedCards { get; set; }
        public int YellowCards { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int MinutesPlayed { get; set; }
        public int Apps { get; set; }
        public int Saves { get; set; }

        public double XG { get; set; }
        public double XA { get; set; }
        public double XG90 { get; set; }
        public double XA90 { get; set; }
    }
}
