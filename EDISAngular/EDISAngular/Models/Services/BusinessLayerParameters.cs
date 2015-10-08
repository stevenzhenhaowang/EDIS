using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Services
{
    public class BusinessLayerParameters
    {

        public const string UserDocumentFolderPrefix = "~/UserDocuments/";


        public const int verificationStatus_NotVerified = 0;
        public const int verificationStatus_Verified = 1;
        public const int verificationStatus_Block = 2;
        public const int feesAndCommissions_RemunerationType = 7;


        public const string clientType_person = "person";
        public const string clientType_entity = "entity";


        public const int noteType_note = 3;
        public const int noteType_message = 1;
        public const int noteType_voice = 4;


        public const int correspondenceSenderRole_adviser = 1;
        public const int correspondenceSenderRole_client = 2;


        public const int transactionType_buy = 1;
        public const int transactionType_sell = 2;
        public const int transactionType_acquaireAsset = 3;





    }
}