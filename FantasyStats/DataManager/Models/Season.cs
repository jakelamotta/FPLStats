using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.Models
{
    public class Season
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        [ForeignKey("League")]
        public int LeagueId { get; set; }
        public virtual League League { get; set; }
        public bool IsCurrent { get; set; }
        public bool DataImported { get; set; }
    }
}
