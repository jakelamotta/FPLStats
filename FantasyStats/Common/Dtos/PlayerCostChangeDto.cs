using System;

namespace Common.Dtos
{
    public class PlayerCostChangeDto
    {
        public int Id { get; set; }
        public PlayerDto Player { get; set; }
        public double Delta { get; set; }
        public DateTime ChangeCaptureDate { get; set; }
    }
}
