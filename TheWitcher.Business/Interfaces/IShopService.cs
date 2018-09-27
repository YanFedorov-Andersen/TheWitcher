using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
