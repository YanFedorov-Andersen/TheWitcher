using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWitcher.Core;
using TheWitcher.Domain.Models;

namespace TheWitcher.Domain.Mappers
{
    public class MapHeroWeapons : IMapper<HeroWeapon, HeroWeaponsDTO>
    {
        public HeroWeaponsDTO AutoMap(HeroWeapon heroWeapon)
        {
            HeroWeaponsDTO heroClothDTO = new HeroWeaponsDTO()
            {
                Id = heroWeapon.Id,
                Description = heroWeapon.Weapons.Characteristics,
                PriceOfSell = heroWeapon.PriceOfSell.Value,
                CombatPower = heroWeapon.Weapons.CombatPower.Value
            };
            return heroClothDTO;
        }
    }
}
