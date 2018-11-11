using Common.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string SecondName { get; set; }

        public int ExternalId { get; set; }
        public double LastCost { get; set; }


        [ForeignKey("Position")]
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }
    }
}
