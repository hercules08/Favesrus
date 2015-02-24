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

        public static string FACEBOOK_PROVIDER = "Facebook";

        public static string CUSTOMER_ROLE = "Customer";
    }
}
