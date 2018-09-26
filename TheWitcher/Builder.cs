using Autofac;
using TheWitcher.DataAccess.Realization;
using TheWitcher.DataAccess.Interfaces;
using TheWitcher.Core;
using TheWitcher.Business;
using TheWitcher.Domain.Mappers;
using DataAccessLayer.Interfaces;

namespace TheWitcher
{
    public class Builder
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            builder.RegisterType<MapHeroes>().As<IMapHeroes>();
            builder.RegisterType<MapQuest>().As<IMapQuest>();

            builder.RegisterType<HeroService>().As<HeroService>();
            builder.RegisterType<QuestService>().As<QuestService>();

            builder.RegisterType<Menu>().As<Menu>();

            var container = builder.Build();

            IUnitOfWork unitOfWork = container.Resolve<IUnitOfWork>();

            IMapHeroes mapHeroes = container.Resolve<IMapHeroes>();
            IMapQuest mapQuest = container.Resolve<IMapQuest>();

            HeroService heroService = container.Resolve<HeroService>();
            QuestService questService = container.Resolve<QuestService>();

            Menu menu = container.Resolve<Menu>();


            menu.Greeting();
            menu.GameMenu();
        }
    }
}