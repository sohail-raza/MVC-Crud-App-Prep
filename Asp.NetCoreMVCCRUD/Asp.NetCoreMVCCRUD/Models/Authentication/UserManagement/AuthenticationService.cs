using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using domAuth = Asp.NetCoreMVCCRUD.Models.Authentication.UserManagement;
using dboAuth = Asp.NetCoreMVCCRUD.Models.Authentication.UserManagement;
using Microsoft.EntityFrameworkCore;
using Asp.NetCoreMVCCRUD.ApiSettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Asp.NetCoreMVCCRUD.Services;

namespace Asp.NetCoreMVCCRUD.Models.Authentication.UserManagement
{
    public class AuthenticationService : ServiceBase<AuthenticationService>, IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly Endpoints _endpoints;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationService(CompanyDatabaseContext dbContext,
            ILogger<AuthenticationService> logger,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IOptions<Endpoints> endpoints,
            SignInManager<ApplicationUser> signInManager)
            : base(dbContext, logger)
        {
            _endpoints = endpoints.Value;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<domAuth.ApplicationUser> GetUserByIdAsync(Guid userId)
        {
            try
            {
                var userIdentity = await _userManager.FindByIdAsync(userId.ToString());
                if (userIdentity == null)
                {
                    return null;
                }

                return _mapper.Map<domAuth.ApplicationUser>(userIdentity);

            }
            catch (Exception exception)
            {
                return null;
            }
        }


        public async Task<ApiResponse<bool>> SigninAsync(domAuth.ApplicationUser userIdentity, string password)
        {
            try
            {
                var dboUser = _mapper.Map<dboAuth.ApplicationUser>(userIdentity);

                if (await _signInManager.CanSignInAsync(dboUser) == false)
                {
                    return new ApiResponse<bool>();
                }

                var signInResult = await _signInManager.CheckPasswordSignInAsync(dboUser, password, false);

                if (!signInResult.Succeeded)
                {
                    return new ApiResponse<bool>();
                }

                return new ApiResponse<bool>(); // LP TODO status code
            }
            catch (Exception exception)
            {
                return new ApiResponse<bool>();// LP TODO status code
            }
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string emailAddress)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(emailAddress);
                if (user == null)
                {
                    return null;
                }

                return _mapper.Map<domAuth.ApplicationUser>(user);

            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }
}
