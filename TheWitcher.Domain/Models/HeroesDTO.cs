using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
