using AutoMapper;
using Favesrus.Data.Dtos;
using Favesrus.Domain.Entity;
using Favesrus.Server.Infrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Favesrus.Server.Dto.FavesrusUser.MapperConfig
{
    public class DtoWishlist_Wishlist:IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<WishListModel, WishList>();
        }
    }
}