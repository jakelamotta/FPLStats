using AutoMapper;
using DataManager.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.EntityManager
{
    public class DataManagerBase
    {
        protected static IMapper Mapper;

        public DataManagerBase()
        {
            AutoMapperConfigurationManager mapper;

            if (Mapper == null)
            {
                mapper = new AutoMapperConfigurationManager();
                Mapper = mapper.Mapper;
            }
           
        }
    }
}
