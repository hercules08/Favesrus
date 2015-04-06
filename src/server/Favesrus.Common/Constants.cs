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
        public static string EMAIL_ADDRESS = "damola.omatosho@gmail.com";

        public static class Status
        {
            public static string FACEBOOK_USER_CREATED = "facebook_user_created";
            public static string INVALID_MODELSTATE = "invalid_modelstate";
            public static string ROOT_OBJECT_NOT_FOUND = "root_object_not_found";
            public static string CHILD_OBJECT_NOT_FOUND = "child_object_not_found";
            public static string FAVES_USER_REGISTERED = "favesrus_user_registered";
            public static string UNABLE_TO_CREATE_USER = "unable_to_create_favesrus_user";
            public static string UNABLE_TO_ADD_USER_TO_ROLE = "unable_to_add_user_to_role";
        }

        public static class MediaTypeNames
        {
            public const string ApplicationXml = "application/xml";
            public const string TextXml = "text/xml";
            public const string ApplicationJson = "application/json";
            public const string TextJson = "text/json";
        }

        public static class Paging
        {
            public const int MinPageSize = 1;
            public const int MinPageNumber = 1;
            public const int DefaultPageNumber = 1;
        }

        public static class CommonParameterNames
        {
            public const string PageNumber = "pageNumber";
            public const string PageSize = "pageSize";
        }

        public static class CommonLinkRelValues
        {
            public const string Self = "self";
            public const string All = "all";
            public const string CurrentPage = "currentPage";
            public const string PreviousPage = "previousPage";
            public const string NextPage = "nextPage";
        }

        public static class CommonRoutingDefinitions
        {
            public const string ApiSegmentName = "api";
            public const string ApiVersionSegmentName = "apiVersion";
            public const string CurrentApiVersion = "v1";
        }

    }
}
