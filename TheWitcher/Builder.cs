using Autofac;
using TheWitcher.DataAccess.Realization;
using TheWitcher.DataAccess.Interfaces;
using TheWitcher.Core;
using TheWitcher.Business;
using TheWitcher.Domain.Mappers;
using DataAccessLayer.Interfaces;
using TheWitcher.Domain.Models;

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

            builder.RegisterType<HeroService>().As<IHeroService>();
            builder.RegisterType<QuestService>().As<IQuestService>();

            builder.RegisterType<Menu>().As<Menu>();

            var container = builder.Build();

            IUnitOfWork unitOfWork = container.Resolve<IUnitOfWork>();

            IMapper<Heroes, HeroesDTO> mapHeroes = container.Resolve<IMapper<Heroes, HeroesDTO>>();
            IMapper<Quest, QuestDTO> mapQuest = container.Resolve<IMapper<Quest, QuestDTO>>();

            IHeroService heroService = container.Resolve<IHeroService>();
            IQuestService questService = container.Resolve<IQuestService>();

            Menu menu = container.Resolve<Menu>();


            menu.Greeting();
            menu.GameMenu();
        }
    }
}