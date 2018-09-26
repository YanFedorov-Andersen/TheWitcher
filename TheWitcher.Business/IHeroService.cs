using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWitcher.Domain.Models;

namespace TheWitcher.Business
{
    public interface IHeroService
    {
        HeroesDTO GetHeroDTO(int heroId);
        void CheckHeroQuests(int heroId);
        bool TakeTheQuest(int heroId, int questId);

    }
}
