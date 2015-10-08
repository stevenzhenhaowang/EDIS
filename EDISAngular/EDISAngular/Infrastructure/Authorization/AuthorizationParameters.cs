using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Infrastructure.Authorization
{
    public class AuthorizationProviders
    {
        /// <summary>
        /// Dynamic provider will provide authorization claims during user logins.
        /// Any claims provided by this provider are not persisted in database.
        /// Any claims provided by this provider are dynamic validated.
        /// Any logics applied to claims of this provider will be dynamically altered during logins.
        /// </summary>
        public const string EdisDynamicProvider = "EDIS System Login Gateway";

        /// <summary>
        /// This is the default persistent claim provider from Identity.
        /// Persistent Claims can be assigned to users and persisted into data store.
        /// Persistent claims will be attached to relevant users until removed from database.
        /// Logins will not effect these claims.
        /// </summary>
        public const string EdisPersistentClaimProvider = "LOCAL AUTHORITY";
    }


    public class AuthorizationRoles
    {
        public const string Role_Preadviser = "preadvisor";
        public const string Role_Adviser = "advisor";
        public const string Role_Preclient = "preclient";
        public const string Role_Client = "client";
        public const string Role_Administrator = "administrator";
        public const string Role_GroupLeader = "groupLeader";

    }

    public class AuthorizationClaims
    {
        public const string ClaimType_VerificationStatus = "VerificationStatus";
        public const string ClaimValue_VerificationStatus_Verified = "Verified";
        public const string ClaimValue_VerificationStatus_Blocked = "Blocked";
        public const string ClaimValue_VerificationStatus_Pending = "Pending";
    }


}