using Autofac;
using TheWitcher.DataAccess.Realization;
using TheWitcher.DataAccess.Interfaces;
using TheWitcher.Core;
using TheWitcher.Business;
using TheWitcher.Domain.Mappers;

namespace TheWitcher
{
    public class Builder
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UnitOfWork>().As<UnitOfWork>();

            builder.RegisterType<MapHeroes>().As<MapHeroes>();
            builder.RegisterType<MapQuest>().As<MapQuest>();

            builder.RegisterType<HeroService>().As<HeroService>();
            builder.RegisterType<QuestService>().As<QuestService>();

            builder.RegisterType<Menu>().As<Menu>();

            var container = builder.Build();

            UnitOfWork unitOfWork = container.Resolve<UnitOfWork>();

            MapHeroes mapHeroes = container.Resolve<MapHeroes>();
            MapQuest mapQuest = container.Resolve<MapQuest>();

            HeroService heroService = container.Resolve<HeroService>();
            QuestService questService = container.Resolve<QuestService>();

            Menu menu = container.Resolve<Menu>();


            menu.Greeting();
            menu.GameMenu();
        }
    }
}