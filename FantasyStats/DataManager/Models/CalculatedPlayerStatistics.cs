using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Models
{
    public class CalculatedPlayerStatistics
    {
        [Key]
        public string Name { get; set; }
        public double xPAbs { get; set; }
        public double xPPound90 { get; set; }
        public double xPPoundMinPlayed { get; set; }
        public int MinutesPlayed { get; set; }
        public double xYc { get; set; }
        public double xRc { get; set; }
    }
}
