using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favit.BLL
{
    public class DebuggingService
    {
        private static bool debugging;

        public static bool RunningInDebugMode()
        {
            //#if DEBUG
            //return true;
            //#else
            //return false;
            //#endif
            WellAreWe();
            return debugging;
        }

        [Conditional("DEBUG")]
        private static void WellAreWe()
        {
            debugging = true;
        }
    }
}
