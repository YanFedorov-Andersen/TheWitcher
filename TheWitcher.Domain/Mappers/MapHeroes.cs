using AutoMapper;
using DataAccessLayer.Core;
using TheWitcher.Domain.Models;

namespace TheWitcher.Domain.Mappers
{
    public class MapHeroes
    {
        public void AutoMapHeroes(Heroes hero, HeroesDTO heroesDTO)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<HeroesDTO, Heroes>()
            .ForMember(x => x.HeroName, x => x.MapFrom(m => m.HeroName))
            .ForMember(x => x.HeroLevel, x => x.MapFrom(m => m.HeroLevel))
            .ForMember(x => x.AvailableWeight, x => x.MapFrom(m =>
            m.AvailableWeight)));
        }
    }
}
