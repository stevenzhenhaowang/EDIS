using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using EDISAngular.Services;





namespace EDISAngular.Infrastructure.Authorization.ClaimProviders
{
    public class AdministrationClaimsProvider
    {

        #warning this class is yet to be implemented to be specific for Peter's account
        /// <summary>
        /// Incomplete implementation, currently will return administration role for every login.
        /// </summary>
        /// <param name="user">User needed to be verified with new claims</param>
        /// <returns></returns>
        public static IEnumerable<Claim> GetClaims(ClaimsIdentity user)
        {
            List<Claim> claims = new List<Claim>();
           
            claims.Add(CreateClaim(ClaimTypes.Role, AuthorizationRoles.Role_Administrator));
            return claims;
        }

        private static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String, AuthorizationProviders.EdisDynamicProvider);
        }
    }
}