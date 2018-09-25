using System;

namespace TheWitcher.Domain.Models
{
    public class QuestDTO
    {
        public int Id { get; set; }
        public string QuestName { get; set; }
        public TimeSpan LeadTime { get; set; }
        public int Complexity { get; set; }
    }
}
