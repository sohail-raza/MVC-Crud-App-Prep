using Asp.NetCoreMVCCRUD.Models;
using Asp.NetCoreMVCCRUD.Models.Authentication;
using Asp.NetCoreMVCCRUD.Models.Authentication.UserManagement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Factories
{
    public interface IJwtFactory
    {

        Task<ClaimsIdentity> GetClaimsIdentityForUser(ApplicationUser user);
        Task<ClaimsIdentity> GetClaimsIdentityForApiClient(string clientId);
        Task<string> GenerateEncodedTokenForUser(string userName, ClaimsIdentity identity);
        Task<Token> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string identifier, JsonSerializerSettings serializerSettings);
        Task<Token> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, Guid identifier, JsonSerializerSettings serializerSettings);
        ClaimsIdentity GenerateClaimsIdentity(string identifier, string userIdentifier);

    }
}
