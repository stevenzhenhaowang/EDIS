using EDIS_DOMAIN;
using EDIS_DOMAIN.Enum.Enums;
using EDISAngular.Infrastructure.DbFirst;
using EDISAngular.Models.BindingModels;
using EDISAngular.Models.ViewModels;
using EDISAngular.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EDISAngular.Infrastructure.DatabaseAccess
{
    public class CorrespondenceRepository
    {
        private edisDbEntities db;
        public CorrespondenceRepository()
        {
            db = new edisDbEntities();
        }
        public CorrespondenceRepository(edisDbEntities database)
        {
            db = database;
        }

        public void SaveResourceFile(string resourceToken, HttpPostedFile postedFile)
        {
            var extension = System.IO.Path.GetExtension(postedFile.FileName);
            var fileName = BusinessLayerParameters.UserDocumentFolderPrefix + Guid.NewGuid().ToString() + extension;
            var filePath = HttpContext.Current.Server.MapPath(fileName);
            postedFile.SaveAs(filePath);
            var resource = db.ResourcesReferences.FirstOrDefault(s => s.TokenValue == resourceToken);
            if (resource != null)
            {
                resource.FileExtension = extension;
                resource.ResourceUrl = fileName;
                db.SaveChanges();
            }
        }
        public string CreateNewMessageToken(string userId)
        {
            EDISAngular.Infrastructure.DbFirst.ResourcesReference resource = new EDISAngular.Infrastructure.DbFirst.ResourcesReference()
            {
                Id = Guid.NewGuid().ToString(),
                FileExtension = "mp3",


                ResourceUrl = "",
                TokenValue = Guid.NewGuid().ToString(),
                UserId = userId
            };
            db.ResourcesReferences.Add(resource);
            db.SaveChanges();

            return resource.TokenValue;
        }
        public async Task RemoveResourceFile(string resourceToken)
        {
            var resource = db.ResourcesReferences.FirstOrDefault(r => r.TokenValue == resourceToken);
            if (resource != null)
            {
                if (!string.IsNullOrEmpty(resource.ResourceUrl))
                {
                    if (File.Exists(HttpContext.Current.Server.MapPath(resource.ResourceUrl)))
                    {
                        File.Delete(HttpContext.Current.Server.MapPath(resource.ResourceUrl));
                    }
                }
                resource.ResourceUrl = "";
                await db.SaveChangesAsync();
            }
        }
        public void CreateNewMessageSync(CorrespondenceNoteBindingModel message, int senderRole)
        {
            var resource = db.ResourcesReferences.SingleOrDefault(r => r.TokenValue == message.resourceToken);
            EDISAngular.Infrastructure.DbFirst.Note note = new EDISAngular.Infrastructure.DbFirst.Note()
            {
                AccountId = "",
                AdviserId = message.adviserNumber,
                AssetTypeId = message.assetTypeId,
                Body = message.body,
                ClientId = message.clientId,
                DateCompleted = message.dateCompleted,
                DateCreated = DateTime.Now,
                DateDue = message.dateDue,
                DateModified = DateTime.Now,
                FollowupActions = message.followupActions,
                FollowupDate = message.followupDate,
                IsAccepted = message.isAccepted,
                IsDeclined = message.isDeclined,
                NoteType = message.noteTypeId,
                NoteId = Guid.NewGuid().ToString(),
                Subject = message.subject,
                SenderRole = senderRole
            };
            db.Notes.Add(note);
            if (!string.IsNullOrEmpty(resource.ResourceUrl))
            {
                EDISAngular.Infrastructure.DbFirst.Attachment attachment = new EDISAngular.Infrastructure.DbFirst.Attachment()
                {
                    AttachmentId = Guid.NewGuid().ToString(),
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    NoteId = note.NoteId,
                    Path = resource.ResourceUrl,
                    Title = note.Subject,
                    AttachmentType = resource.FileExtension
                };
                db.Attachments.Add(attachment);
            }

            db.SaveChanges();
        }
        public async Task CreateMessageFollowup(CorrespondenceFollowupBindingModel model, int senderRole)
        {
            var correspondingNote = db.Notes.SingleOrDefault(n => n.NoteId == model.existingNoteId);
            EDISAngular.Infrastructure.DbFirst.Note note = new EDISAngular.Infrastructure.DbFirst.Note()
            {
                NoteId = Guid.NewGuid().ToString(),
                AdviserId= correspondingNote.AdviserId,
                AccountId = correspondingNote.AccountId,
                AssetClass = correspondingNote.AssetClass,
                AssetTypeId = correspondingNote.AssetTypeId,
                Body = model.body,
                ClientId = correspondingNote.ClientId,
                DateCompleted = correspondingNote.DateCompleted,
                DateCreated = DateTime.Now,
                DateDue = correspondingNote.DateDue,
                DateModified = DateTime.Now,
                NoteType = correspondingNote.NoteType,
                Subject = correspondingNote.Subject,
                SenderRole = senderRole
            };

            EDISAngular.Infrastructure.DbFirst.NoteLink link = new EDISAngular.Infrastructure.DbFirst.NoteLink()
            {
                DateCreated = DateTime.Now,
                NoteId1 = correspondingNote.NoteId,
                NoteId2 = note.NoteId
            };
            db.Notes.Add(note);
            db.NoteLinks.Add(link);
            await db.SaveChangesAsync();
        }
        public List<CorrespondenceView> GetNotesForAdviserByUserId(string adviserUserId, int noteType)
        {
            List<CorrespondenceView> result = new List<CorrespondenceView>();
            var adviser = db.Advisers.SingleOrDefault(ad => ad.AdviserNumber == adviserUserId);
            var adviserId = adviser.AdviserId.ToString();
            var relevantNoteType = noteType;
            foreach (var note in db.Notes.Where(n => n.AccountId == adviserId &&
                n.NoteType == relevantNoteType && db.NoteLinks.Where(l => l.NoteId2 == n.NoteId).Count() == 0))
            {
                var client = db.Clients.SingleOrDefault(c => c.ClientId == note.Client.ClientId);
                var resource = note.Attachments.FirstOrDefault();
                #region create correspondence payload skeleton first
                CorrespondenceView item = new CorrespondenceView()
                {
                    adviserId = adviser.AdviserId.ToString(),
                    adviserName = adviser.FirstName + " " + adviser.LastName,
                    clientId = note.ClientId,
                    clientName = client.ClientType == BusinessLayerParameters.clientType_person ? client.FirstName + " " + client.LastName : client.EntityName,
                    date = note.DateCreated,
                    noteId = note.NoteId,
                    path = resource == null ? "" : System.Web.VirtualPathUtility.ToAbsolute(resource.Path),
                    subject = note.Subject,
                    typeName = CommonHelpers.GetEnumDescription((NoteTypes)note.NoteType),
                    type = resource == null ? "" : resource.AttachmentType,
                    conversations = new List<CorrespondenceConversation>(),
                    actionsRequired = note.FollowupActions,
                    assetClass = note.AssetClass,
                    completionDate = note.DateCompleted,
                    productClass = note.ProductClass
                };
                #endregion
                #region inject the initial conversation

                CorrespondenceConversation initial = new CorrespondenceConversation()
                {
                    content = note.Body,
                    createdOn = note.DateCreated,
                    senderRole = note.SenderRole,
                    senderName = note.SenderRole == BusinessLayerParameters.correspondenceSenderRole_adviser ? adviser.FirstName + " " + adviser.LastName
                    : (client.ClientType == BusinessLayerParameters.clientType_person ? client.FirstName + " " + client.LastName : client.EntityName)
                };
                item.conversations.Add(initial);
                #endregion
                #region insert all other conversations
                foreach (var subnote in db.NoteLinks.Where(n => n.NoteId1 == note.NoteId))
                {
                    var subNoteContent = db.Notes.SingleOrDefault(n => n.NoteId == subnote.NoteId2);
                    CorrespondenceConversation conversation = new CorrespondenceConversation()
                    {
                        content = subNoteContent.Body,
                        senderRole = subNoteContent.SenderRole,
                        createdOn = subNoteContent.DateCreated,
                        senderName = subNoteContent.SenderRole == BusinessLayerParameters.correspondenceSenderRole_adviser ? adviser.FirstName + " " + adviser.LastName
                        : (client.ClientType == BusinessLayerParameters.clientType_person ? client.FirstName + " " + client.LastName : client.EntityName)
                    };
                    item.conversations.Add(conversation);
                }


                #endregion
                item.conversations = item.conversations.OrderBy(s => s.createdOn).ToList();
                result.Add(item);
            }
            return result;
        }
        public List<CorrespondenceView> GetNotesForClientByUserId(string clientUserId, int noteType)
        {
            List<CorrespondenceView> result = new List<CorrespondenceView>();
            var client = db.Clients.SingleOrDefault(c => c.ClientId == clientUserId);
            var clientId = db.Clients.SingleOrDefault(c => c.ClientId == clientUserId).ClientId;
            var relevantNoteType = noteType;
            foreach (var note in db.Notes.Where(n => n.ClientId == clientId &&
                n.NoteType == relevantNoteType && db.NoteLinks.Where(l => l.NoteId2 == n.NoteId).Count() == 0))
            {
                var adviser = db.Advisers.FirstOrDefault(ad => ad.AdviserId.ToString() == note.AccountId);
                var resource = note.Attachments.FirstOrDefault();
                #region create correspondence payload skeleton first
                CorrespondenceView item = new CorrespondenceView()
                {
                    adviserId = adviser.AdviserNumber.ToString(),
                    adviserName = adviser.FirstName + " " + adviser.LastName,
                    clientId = note.ClientId,
                    clientName = client.ClientType == BusinessLayerParameters.clientType_person ? client.FirstName + " " + client.LastName : client.EntityName,
                    date = note.DateCreated,
                    noteId = note.NoteId,
                    path = resource == null ? "" : System.Web.VirtualPathUtility.ToAbsolute(resource.Path),
                    subject = note.Subject,
                    typeName = CommonHelpers.GetEnumDescription((NoteTypes)note.NoteType),
                    type = resource == null ? "" : resource.AttachmentType,
                    conversations = new List<CorrespondenceConversation>(),
                    actionsRequired = note.FollowupActions,
                    assetClass = note.AssetClass,
                    completionDate = note.DateCompleted,
                    productClass = note.ProductClass
                };
                #endregion
                #region inject the initial conversation

                CorrespondenceConversation initial = new CorrespondenceConversation()
                {
                    content = note.Body,
                    createdOn = note.DateCreated,
                    senderRole = note.SenderRole,
                    senderName = note.SenderRole == BusinessLayerParameters.correspondenceSenderRole_adviser ? adviser.FirstName + " " + adviser.LastName
                    : (client.ClientType == BusinessLayerParameters.clientType_person ? client.FirstName + " " + client.LastName : client.EntityName)
                };
                item.conversations.Add(initial);
                #endregion
                #region insert all other conversations
                foreach (var subnote in db.NoteLinks.Where(n => n.NoteId1 == note.NoteId))
                {
                    var subNoteContent = db.Notes.SingleOrDefault(n => n.NoteId == subnote.NoteId2);
                    CorrespondenceConversation conversation = new CorrespondenceConversation()
                    {
                        content = subNoteContent.Body,
                        senderRole = subNoteContent.SenderRole,
                        createdOn = subNoteContent.DateCreated,
                        senderName = subNoteContent.SenderRole == BusinessLayerParameters.correspondenceSenderRole_adviser ? adviser.FirstName + " " + adviser.LastName
                        : (client.ClientType == BusinessLayerParameters.clientType_person ? client.FirstName + " " + client.LastName : client.EntityName)
                    };
                    item.conversations.Add(conversation);
                }


                #endregion
                item.conversations = item.conversations.OrderBy(s => s.createdOn).ToList();
                result.Add(item);
            }
            return result;
        }




    }
}