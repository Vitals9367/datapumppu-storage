﻿namespace Storage.Repositories.Models
{
    public class Meeting
    {
        public DateTime MeetingDate { get; set; }

        public string MeetingID { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string? MeetingTitleFI { get; set; }

        public string? MeetingTitleSV { get; set; }

        public DateTime? Started { get; set; }

        public Guid? MeetingStartedEventID { get; set; }

        public DateTime? Ended { get; set; }

        public Guid? MeetingEndedEventID { get; set; }
    }
}
