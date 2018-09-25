using System;

namespace TheWitcher.Domain.Models
{
    public class HeroesDTO
    {
        public int Id { get; set; }
        public string HeroName { get; set; }
        public int HeroLevel { get; set; }
        public string HeroDescription { get; set; }
        public int AvailableWeight { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int HeroTotalPower { get; set; }
        public decimal HeroMoney { get; set; }
    }
}
