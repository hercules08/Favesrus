using AutoMapper;
using Favesrus.Server.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Dto.FavesrusUser.MapperConfig
{
    public class RegisterModel_FavesrusUser : IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<RegisterModel, Favesrus.Domain.Entity.FavesrusUser>();
        }
    }
}