using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using WAVE.AdminWebsite.Models;
using WebMatrix.WebData;

namespace WAVE.AdminWebsite.App_Start
{
    public class MembershipConfig
    {

        public static void RegisterMembership()
        {
            Database.SetInitializer<UsersContext>(null);
#if DEBUG
            WebSecurity.InitializeDatabaseConnection("SQLServer", "Accounts", "Id", "UserName", autoCreateTables: true);
#else
            WebSecurity.InitializeDatabaseConnection("SQLServerRelease", "Accounts", "Id", "UserName", autoCreateTables: true);
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