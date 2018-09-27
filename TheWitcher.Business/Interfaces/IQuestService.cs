using System.Collections.Generic;
using TheWitcher.Domain.Models;

namespace TheWitcher.Business.Interfaces
{
    public interface IQuestService
    {
        List<QuestDTO> GetNameIdLeadTimeQuests(HeroesDTO hero);
    }
}
