using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManager.AutoMapper
{
    public class Map
    {
        public static TDest To<TSource, TDest>(TSource source, IMapper mapper)
        {
            return mapper.Map<TSource, TDest>(source);
        }
    }

    public static class MapExtension
    {
        public static IList<TDto> ToDtoList<TDto>(this IEnumerable<object> entities, IMapper mapper)
        {
            if (entities == null)
            {
                return new List<TDto>();
            }
            return entities.Select(item => item.ToDto<TDto>(mapper)).ToList();
        }

        public static TDto ToDto<TDto>(this object entity, IMapper mapper)
        {
            return (TDto)mapper.Map(entity, entity.GetType(), typeof(TDto));
        }
    }
}
