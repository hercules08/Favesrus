using Faves = Favesrus.Common;
using Favesrus.Model.Entity;
using Favesrus.Server.Dto.FavesrusUser;
using Favesrus.Server.Infrastructure.Interface;
using Favesrus.Server.Processing.ProcessingFavesrusUser.Interface;
using Favesrus.Services;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Owin;
using System.Threading.Tasks;
using Microsoft.Owin.Host.SystemWeb;
using System.Web;
using Favesrus.Server.Exceptions;
using Favesrus.Common;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using Favesrus.Server.Filters;

namespace Favesrus.Server.Processing.ProcessingFavesrusUser.Implmentation
{
    public class FavesrusUserProcessor : IFavesrusUserProcessor
    {
        private FavesrusUserManager _userManager;
        private FavesrusRoleManager _roleManager;
        private readonly IAutoMapper _mapper;

        public FavesrusUserProcessor(
            IAutoMapper mapper,
            FavesrusUserManager userManager,
            FavesrusRoleManager roleManager
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public FavesrusUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<FavesrusUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public FavesrusRoleManager RoleManager
        {
            get
            {
                return _roleManager ??
                    HttpContext.Current.GetOwinContext()
                    .GetUserManager<FavesrusRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public DtoFavesrusUser RegisterUser(RegisterModel model)
        {
            FavesrusUser user = _mapper.Map<FavesrusUser>(model);
            user.UserName = model.Email; // In Faves user name is email addy.            

            IdentityResult step_1_result;
            IdentityResult step_2_result;
            DtoFavesrusUser dtoFavesrusUser;


            // Step 1 - Create the user
            step_1_result = UserManager.CreateAsync(user, model.Password).Result;
            if (!step_1_result.Succeeded)
            {
                var errors = GetErrorsFromIdentityResult(step_1_result);
                throw new BusinessRuleException(
                    Faves.Constants.Status.UNABLE_TO_CREATE_USER,
                    "Unable to register new Faves 'R' Us user.",
                    errors);
            }

            // Step 2 - Add the user to the customer role
            step_2_result = UserManager.AddToRoleAsync(user.Id, Faves.Constants.CUSTOMER_ROLE).Result;
            if(!step_2_result.Succeeded)
            {
                var errors = GetErrorsFromIdentityResult(step_1_result);
                throw new BusinessRuleException(
                    Faves.Constants.Status.UNABLE_TO_ADD_USER_TO_ROLE,
                    "Unable to add the newly created user to the customer role.",
                    errors);
            }

            // End Result
            dtoFavesrusUser = _mapper.Map<DtoFavesrusUser>(user);

            return dtoFavesrusUser;
        }

        public Task<DtoFavesrusUser> RegisterUserAsync(RegisterModel model)
        {
            return System.Threading.Tasks.Task.FromResult(RegisterUser(model));
        }

        private List<InvalidModelProperty> GetErrorsFromIdentityResult(IdentityResult result)
        {
            List<InvalidModelProperty> invalidModelStates = new List<InvalidModelProperty>();
            int i = 1;

            foreach (string error in result.Errors)
            {
                InvalidModelProperty invalidItem = new InvalidModelProperty();

                invalidItem.ErrorItem = "issue" + i;
                invalidItem.Reason = error;
                i++;
            }

            return invalidModelStates;
        }

        //private void AddErrors(IdentityResult result)
        //{
        //    foreach (var error in result.Errors)
        //    {
        //        int separatorIndex = model.Key.IndexOf(".") + 1; //breaks up the 'model.ProviderKey' to just 'ProviderKey'
        //        string modelKey = model.Key;
        //        InvalidModelProperty invalidItem = new InvalidModelProperty();
        //        invalidItem.ErrorItem = modelKey.Remove(0, separatorIndex);
        //        invalidItem.Reason = model.Value.Errors[0].ErrorMessage;
        //        invalidModelStates.Add(invalidItem);

        //        ModelState.AddModelError("", error);
        //    }
        //}

        //private void AddErrorsFromResult(IdentityResult result)
        //{
        //    foreach (string error in result.Errors)
        //    {
        //        ModelState.AddModelError("", error);
        //    }
        //}

        //private IHttpActionResult GetErrorResult(IdentityResult result)
        //{
        //    if (result == null)
        //    {
        //        return InternalServerError();
        //    }

        //    if (!result.Succeeded)
        //    {
        //        if (result.Errors != null)
        //        {
        //            foreach (string error in result.Errors)
        //            {
        //                ModelState.AddModelError("", error);
        //            }
        //        }

        //        if (ModelState.IsValid)
        //        {
        //            // No ModelState errors are available to send, so just return an empty BadRequest.
        //            return BadRequest();
        //        }

        //        return BadRequest(ModelState);
        //    }

        //    return null;
        //}
    }
}