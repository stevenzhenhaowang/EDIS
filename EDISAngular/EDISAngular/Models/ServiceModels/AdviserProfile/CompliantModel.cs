using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.AdviserProfile
{
    public class CompliantModel
    {
        public CompliantFiles CompliantFiles { get; set; }

        public ComplianceOverview ComplianceOverview { get; set; }
    }
    public class CompliantFiles
    {
        public List<CompliantFilesItem> data { get; set; }
        public double total { get; set; }
    }

    public class CompliantFilesItem
    {
        public string Group { get; set; }
        public int Number{ get; set; }
        public List<CompliantFilesClient> Clients { get; set; }
    }


    public class CompliantFilesClient
    {
        public string AccountGroupNumber { get; set; }
        public string AccountName { get; set; }
    }

    public class ComplianceOverview
    {
        public List<ComplianceOverviewItem> data { get; set; }
        public double total { get; set; }
    }

    public class ComplianceOverviewItem
    {
        public string Group { get; set; }
        public int Number { get; set; }
        public List<ComplianceOverviewStock> Stocks { get; set; }
    }
    public class ComplianceOverviewStock
    {
        public string Label { get; set; }
        public string Name { get; set; }
    }
}