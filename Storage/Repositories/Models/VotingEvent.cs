﻿namespace Storage.Repositories.Models
{
    public class VotingEvent
    {
        public string MeetingID { get; set; }

        public Guid EventID { get; set; }

        public int VotingNumber { get; set; }

        public DateTime Timestamp { get; set; }

        public VotingType VotingType { get; set; }

        public string? VotingTypeTextFI { get; set; }

        public string? VotingTypeTextSV { get; set; }

        public string ForTextFI { get; set; }

        public string ForTextSV { get; set; }

        public string ForTitleFI { get; set; }

        public string ForTitleSV { get; set; }

        public string AgainstTextFI { get; set; }

        public string AgainstTextSV { get; set; }

        public string AgainstTitleFI { get; set; }

        public string AgainstTitleSV { get; set; }

        public int? VotesFor { get; set; }

        public int? VotesAgainst { get; set; }

        public int? VotesEmpty { get; set; }

        public int? VotesAbsent { get; set; }

        public List<Vote>? Votes { get; set; }
    }
}
