using Favesrus.Core;
using Favesrus.Core.Logging;
using Favesrus.Core.TypeMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.API.Controllers
{
    public interface IFaveEventController
    {
        //IHttpActionResult
    }

    public class FaveEventController:BaseApiController, IFaveEventController
    {
        public FaveEventController(ILogManager logManager, IAutoMapper autoMapper)
            :base(logManager, autoMapper)
        {

        }
    }
}
