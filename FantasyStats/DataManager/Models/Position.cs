﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.Models
{
    public class Position
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public int AppId { get; set; }
    }
}
