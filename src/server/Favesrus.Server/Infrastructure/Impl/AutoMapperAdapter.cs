using AutoMapper;
using Favesrus.Server.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Infrastructure.Impl
{
    public class AutoMapperAdapter:IAutoMapper
    {
        public T Map<T>(object objectToMap)
        {
            return Mapper.Map<T>(objectToMap);
        }
    }
}