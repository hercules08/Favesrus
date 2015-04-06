using Favesrus.Server.Dto.FavesrusUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Server.Processing.ProcessingFavesrusUser.Interface
{
    public interface IFavesrusUserProcessor
    {
        DtoFavesrusUser RegisterUser(RegisterModel model);
        Task<DtoFavesrusUser> RegisterUserAsync(RegisterModel model);
    }
}
