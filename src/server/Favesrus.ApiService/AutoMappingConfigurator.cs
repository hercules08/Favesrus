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
            
            Mapper.CreateMap<CategoryModel, Category>()
                .ForMember(x => x.CategoryImage, opt => opt.MapFrom(source => source.Image))
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(source => source.Name));

            Mapper.CreateMap<CategoryModel2, Category>()
                .ForMember(x => x.CategoryImage, opt => opt.MapFrom(source => source.Image))
                .ForMember(x => x.CategoryName, opt => opt.MapFrom(source => source.Name));

            Mapper.CreateMap<EntityBaseModel, EntityBase>();
            Mapper.CreateMap<FavesrusUserModel, FavesrusUser>();
            Mapper.CreateMap<FollowUserModel, FollowUser>();


            Mapper.CreateMap<GiftItemModel, GiftItem>();
            Mapper.CreateMap<GiftItemModel2, GiftItem>()
                .ForMember(x => x.ItemName, opt => opt.MapFrom(source => source.Name))
                .ForMember(x => x.ItemPrice, opt => opt.MapFrom(source => source.Price))
                .ForMember(x => x.Category, opt => opt.Ignore());

            Mapper.CreateMap<RecommendationModel, Recommendation>();
            Mapper.CreateMap<RetailerModel, Retailer>();
            
            Mapper.CreateMap<WishListModel, WishList>();
            Mapper.CreateMap<WishListModel2, WishList>();

            Mapper.CreateMap<FaveEventModel, FaveEvent>();
            
        }

        public static void ConfigureDomainToDto()
        {
            Mapper.CreateMap<BaseCategory, BaseCategoryModel>();
            Mapper.CreateMap<Category, CategoryModel>()
                .ForMember(x => x.Image, opt => opt.MapFrom(source => source.CategoryImage))
                .ForMember(x => x.Name, opt => opt.MapFrom(source => source.CategoryName));

            Mapper.CreateMap<Category, CategoryModel2>()
                .ForMember(x => x.Image, opt => opt.MapFrom(source => source.CategoryImage))
                .ForMember(x => x.Name, opt => opt.MapFrom(source => source.CategoryName));

            Mapper.CreateMap<EntityBase, EntityBaseModel>();
            Mapper.CreateMap<FavesrusUser, FavesrusUserModel>();
            Mapper.CreateMap<FollowUser, FollowUserModel>();
            
            Mapper.CreateMap<GiftItem, GiftItemModel>();
            Mapper.CreateMap<GiftItem, GiftItemModel2>()
                .ForMember(x => x.Price, opt => opt.MapFrom(source => source.ItemPrice))
                .ForMember(x => x.Name, opt => opt.MapFrom(source => source.ItemName))
                .ForMember(x => x.Category, opt => opt.MapFrom(source => source.Category.FirstOrDefault().CategoryName));
            
            Mapper.CreateMap<Recommendation, RecommendationModel>();
            Mapper.CreateMap<Retailer, RetailerModel>();
            
            Mapper.CreateMap<WishList, WishListModel>();
            Mapper.CreateMap<WishList, WishListModel2>();

            

            Mapper.CreateMap<FaveEvent, FaveEventModel>();
        }

        public static void Configure()
        {
            ConfigureDtoToDomain();
            ConfigureDomainToDto();
        }
    }
}
