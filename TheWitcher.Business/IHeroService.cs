﻿using System;
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
        bool BuyClothes(int heroId, int clothesId);
        bool BuyWeapons(int heroId, int weaponsId);
        bool SellWeapon(int heroId, int weaponsId);
        bool SellCloth(int heroId, int clothId);

    }
}
