using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Portfolio.Base;
using Domain.Portfolio.Entities.CreationModels;
using Domain.Portfolio.Interfaces;

namespace Domain.Portfolio.Correspondence
{
    public class Message
    {

        public Message(IRepository repo)
        {
        }

        public string adviserNumber { get; set; }

        public string clientId { get; set; }

        public string assetTypeId { get; set; }
        public string productTypeId { get; set; }

        public double timespent { get; set; }
        public string noteSerial { get; set; }//self generated
        public string subject { get; set; }
        public string body { get; set; }
        public string followupActions { get; set; }
        public DateTime dateDue { get; set; }
        public string status { get; set; }//not supplied from client
        public DateTime followupDate { get; set; }
        public DateTime dateCompleted { get; set; }//not supplied from client
        public bool reminder { get; set; }
        public DateTime reminderDate { get; set; }//not supplied from client
        public int noteTypeId { get; set; }
        public bool isAccepted { get; set; }
        public bool isDeclined { get; set; }
        public string accountId { get; set; }

        public string resourceToken { get; set; }
    }
}
