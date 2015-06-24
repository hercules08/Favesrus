using AutoMapper;
using Favesrus.Data.Dtos;
using Favesrus.Server.Infrastructure.Interface;

namespace Favesrus.Server.Dto.FavesrusUser.MapperConfig
{
    public class DtoFavesrusUser_FavesrusUser:IAutoMapperTypeConfigurator
    {
        public void Configure()
        {
            Mapper.CreateMap<FavesrusUserModel, Favesrus.Domain.Entity.FavesrusUser>();
        }
    }
}