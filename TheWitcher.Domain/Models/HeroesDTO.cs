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
        public Nullable<int> HeroLevel { get; set; }
        public string HeroDescription { get; set; }
        public Nullable<int> AvailableWeight { get; set; }
        public Nullable<System.DateTime> ReleaseDate { get; set; }
        public Nullable<int> HeroTotalPower { get; set; }
    }
}
