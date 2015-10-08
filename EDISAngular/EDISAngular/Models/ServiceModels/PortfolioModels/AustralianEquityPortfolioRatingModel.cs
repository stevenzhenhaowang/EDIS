using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class PortfolioRatingModel
    {
        public List<PortfolioRatingItemModel> data { get; set; }
        public double risk { get; set; }
        public double suitability { get; set; }
        public double notSuited { get; set; }
        public string SuitabilityDesc { get; set; }
    }

    public class PortfolioRatingItemModel
    {
        public string name { get; set; }
        public double score { get; set; }
        public double weighting { get; set; }
    }

}