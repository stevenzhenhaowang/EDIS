using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.PortfolioModels
{
    public class EvaluationModel
    {
        public string title { get; set; }
        public double expected { get; set; }
        public double actual { get; set; }
    }
}