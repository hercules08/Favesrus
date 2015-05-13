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
            Mapper.CreateMap<DtoGiftItem, Favesrus.Model.Entity.GiftItem>()
                .ForMember(entity => entity.ItemName, x => x.MapFrom(dto => dto.Name))
                .ForMember(entity => entity.ItemPrice, x => x.MapFrom(dto => dto.Price))
                .ForMember(entity => entity.ItemImage, x => x.MapFrom(dto => dto.Image));
        }
    }
}