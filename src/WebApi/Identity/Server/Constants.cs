namespace WebApi.Identity.Server
{
    public class Constants
    {
        public static string AdminRoleName = "Admin";
        public static string UserRoleName = "User";

        public static string IdentityServerUri = @"https://localhost:44348/core";
        public static string ImplicitClientUri = @"https://localhost:44300/";
        public static string APIClientUri = @"http://localhost:50316/";

        public static string AureliaClientVisualStudio = @"https://localhost:44368";
        public static string AureliaClientDotnetCLI = @"http://localhost:5000";

        //Clients 
        public static string ImplicitClient = @"implicitClient";
        public static string APIClient = @"apiService";

        public static string IdentitySecret = @"secret";
    }
}