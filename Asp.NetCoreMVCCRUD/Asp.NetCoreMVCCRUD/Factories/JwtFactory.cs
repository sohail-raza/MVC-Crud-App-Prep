using Asp.NetCoreMVCCRUD.Enums.Authentication;
using Asp.NetCoreMVCCRUD.Models.Authentication;
using Asp.NetCoreMVCCRUD.Models.Authentication.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Factories
{
    public class JwtFactory : IJwtFactory
    {
        private readonly ILogger<JwtFactory> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public JwtFactory(ILogger<JwtFactory> logger,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        private JwtIssuerOptions CreateJwt()
        {
            var key = "123456789ABCDE123456";
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256);
            return new JwtIssuerOptions()
            {
                Issuer = "jrny-webapi",
                Audience = "http://localhost:50081/",
                SigningCredentials = signingCredentials
            };
        }



        public async Task<ClaimsIdentity> GetClaimsIdentityForApiClient(string clientId)
        {
            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientId))
                return await Task.FromResult<ClaimsIdentity>(null);

            return await Task.FromResult(GenerateClaimsIdentity(clientId, clientId));
        }

        public async Task<string> GenerateEncodedTokenForUser(string userName, ClaimsIdentity identity)
        {
            var claims = new List<Claim>()
            {
                 new Claim(JwtRegisteredClaimNames.Jti, await CreateJwt().JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(CreateJwt().IssuedAt).ToString(), ClaimValueTypes.Integer64),
                 identity.FindFirst(JwtRegisteredClaimNames.Sub),
                 new Claim(JwtClaimIdentifiers.AuthType, ((int)AuthenticationType.IndividualUser).ToString(), ClaimValueTypes.Integer64)
             };

            // get the user and claims the user has
            var user = await _userManager.FindByNameAsync(userName);
            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims); //add any user claims (but probably won't use these in the db)

            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            claims = await GetRoleClaimsForRole(userRoles, claims);

            return CreateAndEncodeJwtToken(claims);
        }

        private string CreateAndEncodeJwtToken(List<Claim> claims)
        {
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes("123456789ABCDE123456")), SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: "jrny-webapi",
                audience: "http://localhost:50081/", // change this
                                                     //key: "123456789ABCDE123456",
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private async Task<List<Claim>> GetRoleClaimsForRole(IList<string> roles, List<Claim> claims)
        {
            foreach (var role in roles)
            {
                //claims.Add(new Claim(ClaimTypes.Role, userRole)); //add the actual role as a claim
                var retrievedRole = await _roleManager.FindByNameAsync(role);
                if (retrievedRole != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(retrievedRole);
                    foreach (Claim roleClaim in roleClaims)
                    {
                        claims.Add(roleClaim); //add the actual role claims
                    }
                }
            }

            return claims;
        }

        public ClaimsIdentity GenerateClaimsIdentity(string identifier, string clientIdenifier)
        {
            return new ClaimsIdentity(new GenericIdentity(identifier, "Token"), new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, clientIdenifier)
            });
        }

        public async Task<Token> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName, JsonSerializerSettings serializerSettings)
        {
            return new Token()
            {
                Id = identity.Claims.Single(c => c.Type == JwtRegisteredClaimNames.Sub).Value,
                TokenType = "Bearer",
                AccessToken = await GenerateEncodedTokenForUser(userName, identity),
                ExpiresIn = (int)CreateJwt().ValidFor.TotalSeconds
            };
        }
        public async Task<Token> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, Guid clientId, JsonSerializerSettings serializerSettings)
        {
            return new Token()
            {
                Id = identity.Claims.Single(c => c.Type == JwtRegisteredClaimNames.Sub).Value,
                TokenType = "Bearer",
                ExpiresIn = (int)CreateJwt().ValidFor.TotalSeconds
            };
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        public async Task<ClaimsIdentity> GetClaimsIdentityForUser(ApplicationUser user)
        {
            if (user == null)
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            return await Task.FromResult(GenerateClaimsIdentity(user.UserName, user.Id.ToString()));
        }
    }
}