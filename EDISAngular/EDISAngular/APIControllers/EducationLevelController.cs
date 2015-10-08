using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EDISAngular;
using EDISAngular.Models.ViewModels;
using EDISAngular.Services;
using EDISAngular.Infrastructure.DatabaseAccess;
namespace EDISAngular.APIControllers
{
    public class EducationLevelController : ApiController
    {
        private CommonReferenceDataRepository repo; 

        public EducationLevelController()
        {
            repo = new CommonReferenceDataRepository();
        }

        public List<EducationLevel> Get()
        {

            return repo.GetAllEducationLevels();

        }
    }
}
