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
            //builder.RegisterType<TheWitcherEntities>().As<TheWitcherEntities>();
            //builder.RegisterType<ClothesRepository>().As<IRepository<Clothes>>();
            //builder.RegisterType<ClothesTypeRepository>().As<IRepository<ClothesType>>();
            //builder.RegisterType<WeaponRepository>().As<IRepository<Weapons>>();
            //builder.RegisterType<WeaponsTypeRepository>().As<IRepository<WeaponsType>>();
            //builder.RegisterType<QuestRepository>().As<IRepository<Quest>>();
            //builder.RegisterType<HeroRepository>().As<IRepository<Heroes>>();
            //builder.RegisterType<HeroInQuestRepository>().As<IRepository<HeroInQuest>>();

            var container = builder.Build();

            UnitOfWork unitOfWork = container.Resolve<UnitOfWork>();
            //TheWitcherEntities dbContext = container.Resolve<TheWitcherEntities>();
            //IRepository<Clothes> clothes = container.Resolve<IRepository<Clothes>>();
            //IRepository<ClothesType> clothesType = container.Resolve<IRepository<ClothesType>>();
            //IRepository<Weapons> weapons = container.Resolve<IRepository<Weapons>>();
            //IRepository<WeaponsType> weaponsType = container.Resolve<IRepository<WeaponsType>>();
            //IRepository<Quest> quest = container.Resolve<IRepository<Quest>>();
            //IRepository<Heroes> heroes = container.Resolve<IRepository<Heroes>>();
            //IRepository<HeroInQuest> heroInQuest = container.Resolve<IRepository<HeroInQuest>>();

            //SeedData.Initialize(dbContext);
            HeroService heroService = new HeroService(unitOfWork);
            heroService.TakeTheQuest(5, 4);
        }
    }
}