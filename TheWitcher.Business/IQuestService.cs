using System.Collections.Generic;
using TheWitcher.Domain.Models;

namespace TheWitcher.Business
{
    public interface IQuestService
    {
        List<QuestDTO> GetNameIdLeadTimeQuests(HeroesDTO hero);
    }
}
