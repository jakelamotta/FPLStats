using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.Models
{
    public class Match
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("HomeTeam"), Column(Order = 0)]
        public int HomeTeamId { get; set; }
        public virtual SeasonTeam HomeTeam { get; set; }

        [ForeignKey("AwayTeam"), Column(Order = 2)]
        public int AwayTeamId { get; set; }
        public virtual SeasonTeam AwayTeam { get; set; }

        public int HomeGoals { get; set; }

        public double HomeXGoals { get; set; }
        public double HomeXGoalsAgainst { get; set; }

        public int AwayGoals { get; set; }

        public double AwayXGoals { get; set; }
        public double AwayXGoalsAgainst { get; set; }

        public DateTime Date { get; set; }
    }
}
