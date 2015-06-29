using AutoMapper;
using Favesrus.Data.Dtos;
using Favesrus.Domain.Base;
using Favesrus.Domain.Entity;
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
            Mapper.CreateMap<BaseCategoryModel, BaseCategory>();
            Mapper.CreateMap<CategoryModel, Category>();
            Mapper.CreateMap<EntityBaseModel, EntityBase>();
            Mapper.CreateMap<FavesrusUserModel, FavesrusUser>();
            Mapper.CreateMap<FollowUserModel, FollowUser>();
            Mapper.CreateMap<GiftItemModel, GiftItem>();
            Mapper.CreateMap<RecommendationModel, Recommendation>();
            Mapper.CreateMap<RetailerModel, Retailer>();
            Mapper.CreateMap<WishListModel, WishList>();

            Mapper.CreateMap<FaveEventModel, FaveEvent>();
            
        }

        public static void ConfigureDomainToDto()
        {
            Mapper.CreateMap<BaseCategory, BaseCategoryModel>();
            Mapper.CreateMap<Category, CategoryModel>();
            Mapper.CreateMap<EntityBase, EntityBaseModel>();
            Mapper.CreateMap<FavesrusUser, FavesrusUserModel>();
            Mapper.CreateMap<FollowUser, FollowUserModel>();
            Mapper.CreateMap<GiftItem, GiftItemModel>();
            Mapper.CreateMap<Recommendation, RecommendationModel>();
            Mapper.CreateMap<Retailer, RetailerModel>();
            Mapper.CreateMap<WishList, WishListModel>();

            Mapper.CreateMap<GiftItem, GiftItemModel2>()
                .ForMember(x => x.Category, opt => opt.MapFrom(source => source.Category.FirstOrDefault().CategoryName));

            Mapper.CreateMap<FaveEvent, FaveEventModel>();
        }

        public static void Configure()
        {
            ConfigureDtoToDomain();
            ConfigureDomainToDto();
        }
    }
}
