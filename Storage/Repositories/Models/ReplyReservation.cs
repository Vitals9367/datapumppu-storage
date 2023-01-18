﻿namespace Storage.Repositories.Models
{
    public class ReplyReservation
    {
        public string MeetingID { get; set; }

        public Guid EventID { get; set; }

        public string Person { get; set; }

        public string AdditionalInfoFI { get; set; }

        public string AdditionalInfoSV { get; set; }
    }
}
