using AutoMapper;

namespace Favesrus.Core.TypeMapping
{
    public interface IAutoMapper
    {
        T Map<T>(object objectToMap);
    }

    public class AutoMapperAdapter : IAutoMapper
    {
        public T Map<T>(object objectToMap)
        {
            return Mapper.Map<T>(objectToMap);
        }
    }
}
