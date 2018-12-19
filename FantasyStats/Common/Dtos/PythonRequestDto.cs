using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos
{
    public class PythonRequestDto
    {
        public string Command { get; set; }
        public string[] Params { get; set; }
    }
}
