using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ViewModels
{
    public class CorrespondenceView
    {
        public string noteId { get; set; }
        public string typeName { get; set; }
        public string adviserName { get; set; }
        public string adviserId { get; set; }
        public string clientName { get; set; }
        public string clientId { get; set; }
        public string subject { get; set; }

        public string assetClass { get; set; }
        public string productClass { get; set; }
        public DateTime? completionDate { get; set; }
        public string actionsRequired { get; set; }

        public List<CorrespondenceConversation> conversations { get; set; }

        public DateTime date { get; set; }
        public string path { get; set; }
        public string type { get; set; }
    }


    public class CorrespondenceConversation
    {
        public string senderName { get; set; }
        public int senderRole { get; set; }
        public string content { get; set; }
        public DateTime createdOn { get; set; }

    }

}