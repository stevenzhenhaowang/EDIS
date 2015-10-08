using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using EDISAngular;
using Owin;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using EDISAngular.Models;
using EDISAngular.Services;
using EDIS_DOMAIN;
using EDISAngular.Infrastructure.DbFirst;
namespace EDISAngular.Infrastructure.Authorization.ClaimProviders
{
    public class ClientGroupLeaderClaimProvider
    {
        public const string ClaimType_ClientGroupLeader = @"http://edis.com.au/claims/client/groupleadership";
        public const string ClaimValue_ClientGroupLeader_Leader = "Leader";
        public const string ClaimValue_ClientGroupLeader_Member = "Member";

        public async static Task<IEnumerable<Claim>> GetClaims(ClaimsIdentity user)
        {
            List<Claim> claims = new List<Claim>();
            using (edisDbEntities db = new edisDbEntities())
            {
                if (user.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == AuthorizationRoles.Role_Client))
                {

                    var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                    var userProfile = await userManager.FindByNameAsync(user.Name);

                    var client = db.Clients.FirstOrDefault(c => c.ClientUserID == userProfile.Id);
                    if (client != null)
                    {
                        var clientGroup = db.ClientGroups.FirstOrDefault(c => c.ClientGroupID == client.ClientGroupID);
                        if (clientGroup != null && clientGroup.MainClientID == client.ClientUserID)
                        {
                            claims.Add(CreateClaim(ClaimType_ClientGroupLeader, ClaimValue_ClientGroupLeader_Leader));
                        }
                        else
                        {
                            claims.Add(CreateClaim(ClaimType_ClientGroupLeader, ClaimValue_ClientGroupLeader_Member));
                        }
                    }
                }
            }
            return claims;
        }

        private static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String, AuthorizationProviders.EdisDynamicProvider);
        }
    }
}