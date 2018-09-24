using Autofac;
using DataAccessLayer.Realization;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Core;
using TheWitcher.Business;

namespace TheWitcher
{
    public class Builder
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UnitOfWork>().As<UnitOfWork>();

            var container = builder.Build();

            UnitOfWork unitOfWork = container.Resolve<UnitOfWork>();

            HeroService heroService = new HeroService(unitOfWork);
            heroService.TakeTheQuest(5, 4);
        }
    }
}