using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.Models
{
    public class SeasonTeam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }
        [ForeignKey("Season")]
        public int SeasonId { get; set; }
        public virtual Season Season { get; set; }

        public int Position { get; set; }
        public int GamesPlayed { get; set; }
        public int Won { get; set; }
        public int Drawn { get; set; }
        public int Lost { get; set; }
        public int Points { get; set; }
        public int CleanSheets { get; set; }

        public double XGFor { get; set; }
        public double XGAgainst { get; set; }
        public double XPoints { get; set; }
    }
}
