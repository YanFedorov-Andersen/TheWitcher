using TheWitcher.Core;
using TheWitcher.Domain.Models;

namespace TheWitcher.Domain.Mappers
{
    public class MapClothes: IMapper<Clothes, ClothesDTO>
    {
        public ClothesDTO AutoMap(Clothes clothes)
        {
            ClothesDTO questDTO = new ClothesDTO()
            {
                Id = clothes.Id,
                ClothesType = clothes.ClothesType.Value,
                PriceOfBuy = clothes.PriceOfBuy.Value,
                Characteristics = clothes.Characteristics,
                Colour = clothes.Colour,
                CombatPower = clothes.CombatPower.Value               
            };
            return questDTO;
        }
    }
}
