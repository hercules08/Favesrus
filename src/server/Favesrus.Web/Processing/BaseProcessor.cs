using Favesrus.Server.Filters;
using Favesrus.Server.Infrastructure.Interface;
using Favesrus.Services;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Reflection;
using System.Web;

namespace Favesrus.Server.Processing
{
    public abstract class BaseProcessor
    {
        readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        FavesrusUserManager _userManager;
        FavesrusRoleManager _roleManager;
        IAuthenticationManager _authManager;
        IEmailService _emailer;
        readonly IAutoMapper _mapper;

        public BaseProcessor(IEmailService emailer, IAutoMapper mapper)
        {
            _emailer = emailer;
            _mapper = mapper;
        }

        public BaseProcessor(
            FavesrusUserManager userManager,
            FavesrusRoleManager roleManager,
            IAuthenticationManager authManager,
            IEmailService emailer,
            IAutoMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authManager = authManager;
            _emailer = emailer;
            _mapper = mapper;
        }

        public IAutoMapper Mapper
        {
            get
            {
                return _mapper;
            }
        }

        public IEmailService Emailer
        {
            get
            {
                return _emailer;
            }
        }

        public ILog Log
        {
            get
            {
                return _log;
            }
        }

        public IAuthenticationManager AuthManager
        {
            get
            {
                if (_authManager == null)
                    AuthManager = HttpContext.Current.GetOwinContext().Authentication;
                return _authManager;
            }
            private set
            {
                _authManager = value;
            }
        }

        public FavesrusUserManager UserManager
        {
            get
            {
                if (_userManager == null)
                    UserManager = HttpContext.Current.GetOwinContext().GetUserManager<FavesrusUserManager>();

                return _userManager;
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
                if (_roleManager == null)
                    RoleManager = HttpContext.Current.GetOwinContext().GetUserManager<FavesrusRoleManager>();
                return _roleManager;
            }
            private set
            {
                _roleManager = value;
            }
        }

        public List<InvalidModelProperty> GetErrorsFromIdentityResult(IdentityResult result)
        {
            List<InvalidModelProperty> invalidModelStates = new List<InvalidModelProperty>();
            int i = 1;

            foreach (string error in result.Errors)
            {
                InvalidModelProperty invalidItem = new InvalidModelProperty();

                invalidItem.ErrorItem = "issue" + i;
                invalidItem.Reason = error;
                invalidModelStates.Add(invalidItem);
                i++;
            }

            return invalidModelStates;
        }
    }
}