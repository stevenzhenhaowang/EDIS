using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using EDISAngular.Models.BindingModels;
using System.Collections.Generic;
using EDISAngular.Models.ViewModels;
using System.Data.Entity;
using System.IO;
using EDISAngular.Services;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using EDISAngular.Infrastructure.Authorization;
using EDIS_DOMAIN;
using EDIS_DOMAIN.Enum.Enums;
using EDISAngular.Infrastructure.DatabaseAccess;
using EDISAngular.Infrastructure.DbFirst;
using SqlRepository;
using Domain.Portfolio.Correspondence;

using SqlRepository;


namespace EDISAngular.APIControllers
{

    public class CorrespondenceController : ApiController
    {
        private EdisRepository edisRepo;
        private CommonReferenceDataRepository comRepo;
        private CorrespondenceRepository corresRepo;
        //private AdviserRepository advRepo;
        private edisDbEntities db;
        private EdisRepository repo;
        public CorrespondenceController()
        {
            edisDbEntities db = new edisDbEntities();
            comRepo = new CommonReferenceDataRepository(db);
            corresRepo = new CorrespondenceRepository(db);
            //advRepo = new AdviserRepository(db);
            edisRepo = new EdisRepository();
        }



        #region common user actions for both client and adviser
        [HttpPost, Route("api/correspondence/file/upload")]
        public async Task<IHttpActionResult> postFiles(string resourceToken)
        {

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count == 1)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    corresRepo.SaveResourceFile(resourceToken, postedFile);
                }
                return Ok();
            }
            else
            {
                return BadRequest();
            }


        }
        [HttpGet, Route("api/correspondence/file/token")]
        public string userGetNewMessageToken()
        {
            return corresRepo.CreateNewMessageToken(User.Identity.GetUserId());
        }
        [HttpPost, Route("api/correspondence/file/remove")]
        public async Task<IHttpActionResult> removeFile(string resourceToken)
        {
            await corresRepo.RemoveResourceFile(resourceToken);
            return Ok();

        }
        [HttpPost, Route("api/correspondence/create")]
        [Authorize(Roles = AuthorizationRoles.Role_Client + "," + AuthorizationRoles.Role_Adviser)]
        public IHttpActionResult createNewNote(CorrespondenceNoteBindingModel message)
        {
            if (message != null && ModelState.IsValid)
            {

                if (User.IsInRole(AuthorizationRoles.Role_Adviser))
                {
                    var adviser = edisRepo.GetAdviserSync(message.adviserNumber, DateTime.Now);
                    if (adviser == null || adviser.AdviserNumber != User.Identity.GetUserId())
                    {
                        ModelState.AddModelError("", "Invalid adviser id supplied, or current adviser is trying to add note for another adviser, which is illegal.");
                        return BadRequest(ModelState);
                    }
                    #region make sure client cannot create notes
                    if (User.IsInRole(AuthorizationRoles.Role_Client) && message.noteTypeId == BusinessLayerParameters.noteType_note)
                    {
                        ModelState.AddModelError("", "Client cannot add self-note.");
                        return BadRequest(ModelState);
                    }
                    var senderRole = User.IsInRole(AuthorizationRoles.Role_Adviser) ?
                                            BusinessLayerParameters.correspondenceSenderRole_adviser
                                            : BusinessLayerParameters.correspondenceSenderRole_client;

                    #endregion

                    Message messageData = new Message(edisRepo) { 
                        adviserNumber = message.adviserNumber,
                        assetTypeId = message.assetTypeId,
                        body = message.body,
                        accountId = message.accountId,
                        clientId = message.clientId,
                        dateCompleted = message.dateCompleted,
                        dateDue = message.dateDue,
                        followupActions = message.followupActions,
                        followupDate = message.followupDate,
                        isAccepted = message.isAccepted,
                        isDeclined = message.isDeclined,
                        noteSerial = message.noteSerial,
                        noteTypeId = message.noteTypeId,
                        productTypeId = message.productTypeId,
                        reminder = message.reminder,
                        reminderDate = DateTime.Now,                    //need to be changed
                        resourceToken = message.resourceToken,
                        status = message.status,
                        subject = message.subject,
                        timespent = message.timespent
                    };

                    edisRepo.CreateNewMessageSync(messageData, senderRole);

                }


                return Ok();


            }
            else
            {
                if (message == null)
                {
                    ModelState.AddModelError("", "Model is not provided");
                }
                return BadRequest(ModelState);
            }
        }

        [HttpPost, Route("api/correspondence/followup")]
        [Authorize(Roles = AuthorizationRoles.Role_Client + "," + AuthorizationRoles.Role_Adviser)]
        public async Task<IHttpActionResult> followUp(CorrespondenceFollowupBindingModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var senderRole = User.IsInRole(AuthorizationRoles.Role_Adviser) ?
                            BusinessLayerParameters.correspondenceSenderRole_adviser
                            : BusinessLayerParameters.correspondenceSenderRole_client;
                await corresRepo.CreateMessageFollowup(model, senderRole);
                return Ok();
            }
            else
            {
                if (model == null)
                {
                    ModelState.AddModelError("", "Model is not provided");
                }
                return BadRequest(ModelState);
            }
        }

        //[HttpGet, Route("api/correspondence/noteType")]
        //[Authorize(Roles = AuthorizationRoles.Role_Client + "," + AuthorizationRoles.Role_Adviser)]
        //public async Task<List<NoteTypeView>> getNoteTypes()
        //{
        //    if (User.IsInRole(AuthorizationRoles.Role_Adviser))
        //    {
        //        return comRepo.GetAllNoteTypes();
        //    }
        //    else
        //    {
        //        return comRepo.GetAllNoteTypes().Where(t => t.id != BusinessLayerParameters.noteType_note).ToList();
        //    }

        //}


        [HttpGet, Route("api/correspondence/noteType")]
        [Authorize(Roles = AuthorizationRoles.Role_Client + "," + AuthorizationRoles.Role_Adviser)]
        public List<NoteTypeView> getNoteTypes()
        {
            if (User.IsInRole(AuthorizationRoles.Role_Adviser))
            {
                return comRepo.GetAllNoteTypes();
            }
            else
            {
                return comRepo.GetAllNoteTypes().Where(t => t.id != BusinessLayerParameters.noteType_note).ToList();
            }

        }
        #endregion

        #region incomplete actions
        /// <summary>
        /// ########################Incomplete##########################################
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("api/correspondence/file")]
        public HttpResponseMessage getFile()
        {

            ///Combine attachment.title and attachment.attachmentType for filename.
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(HttpContext.Current.Server.MapPath("~/UserDocuments/12.jpg"), FileMode.Open);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "file.jpg" };
            return result;
        }
        #endregion

        #region adviser specific actions/helpers
        [HttpGet, Route("api/adviser/correspondence/notes")]
        [Authorize(Roles = AuthorizationRoles.Role_Adviser)]
        public List<CorrespondenceView> getNotes_adviser()
        {
            return getNotesForCurrentAdviser(BusinessLayerParameters.noteType_note);
        }

        [HttpGet, Route("api/adviser/correspondence/messages")]
        [Authorize(Roles = AuthorizationRoles.Role_Adviser)]
        public List<CorrespondenceView> getMessages_adviser()
        {
            return getNotesForCurrentAdviser(BusinessLayerParameters.noteType_message);
        }

        [HttpGet, Route("api/adviser/correspondence/voice")]
        public List<CorrespondenceView> getVoices_adviser()
        {
            return getNotesForCurrentAdviser(BusinessLayerParameters.noteType_voice);
        }

        private List<CorrespondenceView> getNotesForCurrentAdviser(int noteType)
        {

            var userid = User.Identity.GetUserId();
            return corresRepo.GetNotesForAdviserByUserId(userid, noteType);

        }
        #endregion

        #region client specific actions/helpers
        [HttpGet, Route("api/client/correspondence/messages")]
        [Authorize(Roles = AuthorizationRoles.Role_Client)]
        public List<CorrespondenceView> getMessages_client()
        {
            return getNotesForCurrentClient(BusinessLayerParameters.noteType_message);
        }
        [HttpGet, Route("api/client/correspondence/voice")]
        [Authorize(Roles = AuthorizationRoles.Role_Client)]
        public List<CorrespondenceView> getVoices_client()
        {
            return getNotesForCurrentClient(BusinessLayerParameters.noteType_voice);
        }
        private List<CorrespondenceView> getNotesForCurrentClient(int noteType)
        {
            var userid = User.Identity.GetUserId();
            return corresRepo.GetNotesForClientByUserId(userid, noteType);

        }

        #endregion





    }
}
