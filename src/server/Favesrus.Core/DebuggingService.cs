using System.Diagnostics;

namespace Favesrus.Core
{
    /// <summary>
    /// Provides service to check if running in debug or release mode
    /// </summary>
    public class DebuggingService
    {
        private static bool debugging;

        /// <summary>
        /// Checks to if if we are running in debug
        /// </summary>
        /// <returns>True if running in debug. False otherwise.</returns>
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
