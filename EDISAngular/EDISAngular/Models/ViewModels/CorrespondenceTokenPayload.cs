using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ViewModels
{
    public class CorrespondenceTokenPayload
    {
        public string token { get; set; }
        public DateTime time { get; set; }
        public string serialNumber { get; set; }
    }
}