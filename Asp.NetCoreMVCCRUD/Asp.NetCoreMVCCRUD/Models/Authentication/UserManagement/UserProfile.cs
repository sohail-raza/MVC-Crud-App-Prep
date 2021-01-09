using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Models.Authentication.UserManagement
{
    public class UserProfile
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public ApplicationUser User { get; set; }
    }
}
