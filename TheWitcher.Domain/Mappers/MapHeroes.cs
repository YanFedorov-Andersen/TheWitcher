using TheWitcher.Core;
using TheWitcher.Domain.Models;

namespace TheWitcher.Domain.Mappers
{
    public class MapHeroes : IMapHeroes
    {
        public HeroesDTO AutoMapHeroes(Heroes hero)
        {
            HeroesDTO heroDTO = new HeroesDTO()
            {
                Id = hero.Id,
                HeroLevel = hero.HeroLevel.Value,
                HeroName = hero.HeroName,
                HeroDescription = hero.HeroDescription,
                HeroMoney = hero.HeroMoney.Value
            };
            return heroDTO;
        }
    }
}
