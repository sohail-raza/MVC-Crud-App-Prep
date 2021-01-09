using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Models.Authentication.UserManagement
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual Employee Employee { get; set; }
        public IList<ApplicationUserRoles> UserRoles { get; set; }
    }
}
