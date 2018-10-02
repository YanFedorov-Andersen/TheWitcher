using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWitcher.DataAccess.Core;
using TheWitcher.Domain.Models;

namespace TheWitcher.Domain.Mappers
{
    public class MapHeroClothes : IMapper<HeroClothes, HeroClothesDTO>
    {
        public HeroClothesDTO AutoMap(HeroClothes heroCloth)
        {
            HeroClothesDTO heroClothDTO = new HeroClothesDTO()
            {
                Id = heroCloth.Id,
                Description = heroCloth.Clothes.Characteristics,
                PriceOfSell = heroCloth.PriceOfSell.Value,
                CombatPower = heroCloth.Clothes.CombatPower.Value
            };
            return heroClothDTO;
        }
    }
}
