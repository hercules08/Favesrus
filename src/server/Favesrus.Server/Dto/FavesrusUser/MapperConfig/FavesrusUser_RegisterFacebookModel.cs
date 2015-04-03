using AutoMapper;
using Favesrus.Server.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Dto.FavesrusUser.MapperConfig
{
    public class FavesrusUser_RegisterFacebookModel : IAutoMapperTypeConfigurator
    {


        public void Configure()
        {
            Mapper.CreateMap<Favesrus.Model.Entity.FavesrusUser, RegisterFacebookModel>()
                .ForMember(m => m.ProviderKey, x => x.Ignore());
        }
    }
}