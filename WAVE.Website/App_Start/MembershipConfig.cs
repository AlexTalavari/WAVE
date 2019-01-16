using System.Data.Entity;
using System.Web.Security;
using WAVE.Website.Models;
using WebMatrix.WebData;

namespace WAVE.Website.App_Start
{
    public class MembershipConfig
    {
        public static void RegisterMembership()
        {
            Database.SetInitializer<UsersContext>(null);
#if DEBUG
            WebSecurity.InitializeDatabaseConnection("SQLAzureConnection", "Accounts", "Id", "UserName", true);
#else
            WebSecurity.InitializeDatabaseConnection("SQLAzureConnection", "Accounts", "Id", "UserName", autoCreateTables: true);
#endif

            CreateRoles();
            CreateAdmin();
        }

        private static void CreateAdmin()
        {
            if (!WebSecurity.UserExists("alxdjo"))
            {
                WebSecurity.CreateUserAndAccount("alxdjo", "d-power21");
            }

            if (!Roles.IsUserInRole("alxdjo", "Administrator"))
            {
                Roles.AddUserToRole("alxdjo", "Administrator");
            }
        }

        private static void CreateRoles()
        {
            if (!Roles.RoleExists("SuperAdministrator"))
            {
                Roles.CreateRole("SuperAdministrator");
            }
            if (!Roles.RoleExists("Administrator"))
            {
                Roles.CreateRole("Administrator");
            }
            if (!Roles.RoleExists("User"))
            {
                Roles.CreateRole("User");
            }
            if (!Roles.RoleExists("VIP"))
            {
                Roles.CreateRole("VIP");
            }
            if (!Roles.RoleExists("Banned"))
            {
                Roles.CreateRole("Banned");
            }
        }
    }
}