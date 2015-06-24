using AutoMapper;
using Favesrus.Data.Dtos;
using Favesrus.Server.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Dto.GiftItem.MapperConfig
{
    public class GiftItem_DtoGiftItem:IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<Favesrus.Domain.Entity.GiftItem, GiftItemModel>()
                .ForMember(entity => entity.Name, x => x.MapFrom(dto => dto.ItemName))
                .ForMember(entity => entity.Price, x => x.MapFrom(dto => dto.ItemPrice))
                .ForMember(entity => entity.Image, x => x.MapFrom(dto => dto.ItemImage));
        }
    }
}