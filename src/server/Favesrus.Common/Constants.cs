namespace Favesrus.Common
{
    public static class Constants
    {

        public static string DEPLOYED_CONTEXT = "name=DeployContext";

        public static string DB_NAME 
        { 
            get
            {
                if(!DebuggingService.RunningInDebugMode())
                {
                    return DEPLOYED_CONTEXT;
                }
                else
                {
                    return "Favesrus_DEBUG";
                }
            }
        }
    }
}
