using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.RebalanceModels
{
    public class TemplateDetails
    {
        public string templateName { get; set; }
        public string templateId { get; set; }
        public TemplateDetailsCorrespondingProfile correspondingProfile { get; set; }
        //public List<TemplateDetailsGroupParameter> groupParameters { get; set; }
        public List<TemplateDetailsItemParameter> itemParameters { get; set; }
    }


    public class TemplateDetailsCorrespondingProfile
    {
        public string profileName { get; set; }
        public string profileId { get; set; }
    }



    public class TemplateDetailsGroupParameter
    {
        public string groupName { get; set; }
        public string groupId { get; set; }
        public List<string> identityMetakey { get; set; }
        public double currentWeighting { get; set; }
    }

    public class TemplateDetailsItemParameter
    {
        /// <summary>
        /// A list of group ids that this parameter belongs to 
        /// </summary>
        public List<string> groupId { get; set; }
        //parameter name
        public string itemName { get; set; }
        //parameter id
        public string id { get; set; }
        public double marketValue { get; set; }
        public double currentValue { get; set; }
        public double currentWeighting { get; set; }
        /// <summary>
        /// Metadata of this parameter's category structure,in the format of [type]#[subtype]#[sub-subtype]...
        /// </summary>
        public List<string> identityMetaKey{ get; set; }

    }



}