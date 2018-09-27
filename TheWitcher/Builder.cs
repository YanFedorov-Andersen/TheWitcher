using Autofac;
using TheWitcher.DataAccess.Realization;
using TheWitcher.Core;
using TheWitcher.Business;
using TheWitcher.Domain.Mappers;
using DataAccessLayer.Interfaces;
using TheWitcher.Domain.Models;
using TheWitcher.Business.Interfaces;
using TheWitcher.Business.Services;

namespace TheWitcher
{
    public class Builder
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            builder.RegisterType<MapHeroes>().As<IMapper<Heroes, HeroesDTO>>();
            builder.RegisterType<MapQuest>().As<IMapper<Quest, QuestDTO>>();
            builder.RegisterType<MapWeapons>().As<IMapper<Weapons, WeaponsDTO>>();
            builder.RegisterType<MapClothes>().As<IMapper<Clothes, ClothesDTO>>();
            builder.RegisterType<MapHeroClothes>().As<IMapper<HeroClothes, HeroClothesDTO>>();
            builder.RegisterType<MapHeroWeapons>().As<IMapper<HeroWeapon, HeroWeaponsDTO>>();

            builder.RegisterType<HeroService>().As<IHeroService>();
            builder.RegisterType<QuestService>().As<IQuestService>();
            builder.RegisterType<ShopService>().As<IShopService>();

            builder.RegisterType<Menu>().As<Menu>();

            var container = builder.Build();

            IUnitOfWork unitOfWork = container.Resolve<IUnitOfWork>();

            IMapper<Heroes, HeroesDTO> mapHeroes = container.Resolve<IMapper<Heroes, HeroesDTO>>();
            IMapper<Quest, QuestDTO> mapQuest = container.Resolve<IMapper<Quest, QuestDTO>>();
            IMapper<Weapons, WeaponsDTO> mapWeapon = container.Resolve<IMapper<Weapons, WeaponsDTO>>();
            IMapper<Clothes, ClothesDTO> mapClothes = container.Resolve<IMapper<Clothes, ClothesDTO>>();
            IMapper<HeroClothes, HeroClothesDTO> mapHeroClothes = container.Resolve<IMapper<HeroClothes, HeroClothesDTO>>();
            IMapper<HeroWeapon, HeroWeaponsDTO> mapHeroWeapon = container.Resolve<IMapper<HeroWeapon, HeroWeaponsDTO>>();

            IHeroService heroService = container.Resolve<IHeroService>();
            IQuestService questService = container.Resolve<IQuestService>();
            IShopService shopService = container.Resolve<IShopService>();

            Menu menu = container.Resolve<Menu>();


            menu.Greeting();
            menu.GameMenu();
        }
    }
}