using TheWitcher.Domain.Models;

namespace TheWitcher.Business.Interfaces
{
    public interface IHeroService
    {
        HeroesDTO GetHeroDTO(int heroId);
        bool CheckHeroQuests(int heroId);
        bool TakeTheQuest(int heroId, int questId);
        bool BuyClothes(int heroId, int clothesId);
        bool BuyWeapons(int heroId, int weaponsId);
        bool SellWeapon(int heroId, int weaponsId);
        bool SellCloth(int heroId, int clothId);
        int CountPowerOfHero(int id);
    }
}
