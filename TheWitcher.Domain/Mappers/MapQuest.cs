using TheWitcher.Core;
using TheWitcher.Domain.Models;

namespace TheWitcher.Domain.Mappers
{
    public class MapQuest
    {
        public QuestDTO AutoMapQuests(Quest quest)
        {
            QuestDTO questDTO = new QuestDTO()
            {
                Id = quest.Id,
                QuestName = quest.QuestName,
                LeadTime = quest.LeadTime.Value,
                Complexity = quest.Complexity.Value
            };
            return questDTO;               
        }
    }
}
