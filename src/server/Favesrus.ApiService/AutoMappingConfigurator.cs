using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.ApiService
{
    public class AutoMappingConfigurator
    {
        public static void ConfigureDtoToDomain()
        {
            //Mapper.CreateMap<
        }

        public static void ConfigureDomainToDto()
        {

        }

        public static void Configure()
        {
            ConfigureDtoToDomain();
            ConfigureDomainToDto();
        }
    }
}
