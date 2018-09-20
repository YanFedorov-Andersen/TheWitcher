using Autofac;
using DataAccessLayer.Realization;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Core;
namespace TheWitcher
{
    public class Builder
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ClothesRepository>().As<IRepository<Clothes>>();
            builder.RegisterType<ClothesTypeRepository>().As<IRepository<ClothesType>>();
            builder.RegisterType<WeaponRepository>().As<IRepository<Weapons>>();
            builder.RegisterType<WeaponsTypeRepository>().As<IRepository<WeaponsType>>();
            builder.RegisterType<QuestRepository>().As<IRepository<Quest>>();
            builder.RegisterType<HeroRepository>().As<IRepository<Heroes>>();

            var container = builder.Build();

            IRepository<Clothes> clothes = container.Resolve<IRepository<Clothes>>();
            IRepository<ClothesType> clothesType = container.Resolve<IRepository<ClothesType>>();
            IRepository<Weapons> weapons = container.Resolve<IRepository<Weapons>>();
            IRepository<WeaponsType> weaponsType = container.Resolve<IRepository<WeaponsType>>();
            IRepository<Quest> quest = container.Resolve<IRepository<Quest>>();
            IRepository<Heroes> heroes = container.Resolve<IRepository<Heroes>>();

        }
    }
}