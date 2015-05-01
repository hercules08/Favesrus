using AutoMapper;
using Favesrus.Server.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Dto.GiftItem.MapperConfig
{
    public class DtoGiftItem_GiftItem:IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<DtoGiftItem, Favesrus.Model.Entity.GiftItem>();
        }
    }
}