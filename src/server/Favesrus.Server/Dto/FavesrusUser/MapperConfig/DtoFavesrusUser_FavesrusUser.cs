using AutoMapper;
using Favesrus.Server.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Dto.FavesrusUser.MapperConfig
{
    public class DtoFavesrusUser_FavesrusUser:IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<DtoFavesrusUser, Favesrus.Model.Entity.FavesrusUser>();
        }
    }
}