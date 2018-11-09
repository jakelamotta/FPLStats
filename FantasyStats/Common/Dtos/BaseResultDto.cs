using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos
{
    public class BaseResultDto<T>
    {
        public T DataObject { get; set; }
        public bool Status { get; set; }
    }
}
