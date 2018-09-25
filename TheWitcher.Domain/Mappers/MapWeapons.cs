using TheWitcher.Core;
using TheWitcher.Domain.Models;

namespace TheWitcher.Domain.Mappers
{
    public class MapWeapons
    {
        public WeaponsDTO AutoMapQuests(Weapons weapons)
        {
            WeaponsDTO questDTO = new WeaponsDTO()
            {
                Id = weapons.Id,
                WeaponType = weapons.WeaponType.Value,
                PriceOfBuy = weapons.PriceOfBuy.Value,
                Characteristics = weapons.Characteristics,
                CombatPower = weapons.CombatPower.Value
            };
            return questDTO;
        }
    }
}
