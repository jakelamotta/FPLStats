using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataManager.Models
{
    public class ApplicationLog
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(255)]
        public string Username { get; set; }

        [MaxLength(255)]
        public string ApplicationUser { get; set; }

        [MaxLength(50), Required]
        public string Level { get; set; }

        [Required]
        public string Message { get; set; }
        public string Exception { get; set; }

        [Column(TypeName = "ntext")]
        [MaxLength]
        public string ExtendedInformation { get; set; }

        [MaxLength(255)]
        public string SessionId { get; set; }
    }
}
