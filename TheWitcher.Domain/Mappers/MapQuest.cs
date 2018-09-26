using TheWitcher.Core;
using TheWitcher.Domain.Models;

namespace TheWitcher.Domain.Mappers
{
    public class MapQuest: IMapper<Quest, QuestDTO>
    {
        public QuestDTO AutoMap(Quest quest)
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
