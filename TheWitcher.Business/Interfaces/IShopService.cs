using System.Collections.Generic;
using TheWitcher.Domain.Models;

namespace TheWitcher.Business.Interfaces
{
    public interface IShopService
    {
        List<ClothesDTO> GetClothesListForBuy();
        List<WeaponsDTO> GetWeaponsListForBuy();
        List<HeroWeaponsDTO> GetWeaponsListForSell(int heroId);
        List<HeroClothesDTO> GetClothesListForSell(int heroId);
    }
}
