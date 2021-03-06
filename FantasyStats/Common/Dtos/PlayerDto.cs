﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos
{

    public class PlayerDto
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public double LastCost { get; set; }
        public Constants.PositionEnum Position { get; set; }
    }
}
