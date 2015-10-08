using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EDISAngular.Models.ServiceModels.AdviserProfile
{
    public class ReminderModel
    {
        public List<ReminderTimeGroupmodel> Weekly { get; set; }
        public List<ReminderTimeGroupmodel> Monthly { get; set; }
        public List<ReminderTimeGroupmodel> Yearly { get; set; }


    }

    public class ReminderTimeGroupmodel
    {
        public string group { get; set; }
        public List<ReminderTimeGroupDataItem> data { get; set; }
    }


    public class ReminderTimeGroupDataItem
    {
        public string type { get; set; }
        public int number { get; set; }
        public List<ReminderTimeGroupDataContent> contents { get; set; }
    }

    public class ReminderTimeGroupDataContent
    {
        public string accountNumber { get; set; }
        public string accountName { get; set; }
    }

}