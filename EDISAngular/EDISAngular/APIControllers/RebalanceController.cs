using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EDISAngular.Infrastructure.DatabaseAccess;
using EDISAngular.Models.ServiceModels.RebalanceModels;
using Microsoft.AspNet.Identity;

namespace EDISAngular.APIControllers
{
    public class RebalanceController : ApiController
    {
        private RebalanceRepository rebRepo;

        public RebalanceController()
        {
            rebRepo = new RebalanceRepository();
        }

        [HttpGet, Route("api/adviser/models")]
        public List<RebalanceModel> GetAllModelsForAdviser()
        {
            return rebRepo.GetAllModelsForAdviser(User.Identity.GetUserId());
        }

        [HttpGet, Route("api/client/models")]
        public List<RebalanceModel> GetAllModelsForClient()
        {
            return rebRepo.GetAllModelsForClient(User.Identity.GetUserId());
        }



        [HttpGet, Route("api/adviser/model")]
        public RebalanceModel GetModelForId(string modelId)
        {
            return rebRepo.GetModelById(modelId);
        }


        [HttpPost,Route("api/adviser/model/create")]
        public IHttpActionResult CreateNewModel(RebalanceCreationModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                //rebRepo.CreateNewModel(model, User.Identity.GetUserId());
                return Ok(new { modelId= "modelIdValue"});
            }
            return BadRequest();
        }
        [HttpPost,Route("api/adviser/model/remove")]
        public IHttpActionResult RemoveModel([FromBody]string modelId)
        {
            //rebRepo.RemoveModel(modelId);
            return Ok();
        }
        [HttpGet, Route("api/adviser/model/parameters")]
        public List<TemplateDetailsItemParameter> GetAllParamtersForGroup(string groupId)
        {
            return rebRepo.GetAllParametersForGroup(groupId);
        }
        [HttpGet, Route("api/adviser/model/filters")]
        public List<FilterGroupModel> GetFilterGroups()
        {
            return rebRepo.GetFilterGroups(User.Identity.GetUserId());
        }
        [HttpGet, Route("api/adviser/model/profiles")]
        public List<ModelProfile> GetAllModelProfiles()
        {
            return rebRepo.GetAllModelProfiles();
        }
    }
}
