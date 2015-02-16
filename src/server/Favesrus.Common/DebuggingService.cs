using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Common
{
    public class DebuggingService
    {
        private static bool debugging;

        public static bool RunningInDebugMode()
        {
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
