using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWitcher.Core;
using TheWitcher.Domain.Mappers;

namespace TheWitcher.Domain.Models
{
    public class HeroWeaponsDTO 
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal PriceOfSell { get; set; }
        public int CombatPower { get; set; }
    }
}
