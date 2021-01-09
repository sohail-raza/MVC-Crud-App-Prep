using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dom = Asp.NetCoreMVCCRUD.Models.Authentication.UserManagement;


namespace Asp.NetCoreMVCCRUD.Models.Authentication.UserManagement
{
    public interface IAuthenticationService
    {
       
        Task<dom.ApplicationUser> GetUserByEmailAsync(string emailAddress);
        Task<dom.ApplicationUser> GetUserByIdAsync(Guid userId);
        Task<ApiResponse<bool>> SigninAsync(ApplicationUser userIdentity, string password);
    }
}
