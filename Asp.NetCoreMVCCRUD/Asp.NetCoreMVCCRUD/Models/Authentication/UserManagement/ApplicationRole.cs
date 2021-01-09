using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Models.Authentication.UserManagement
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public IList<ApplicationUserRoles> UserRoles { get; set; }
    }
}
