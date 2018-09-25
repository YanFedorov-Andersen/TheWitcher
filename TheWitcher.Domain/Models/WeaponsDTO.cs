namespace TheWitcher.Domain.Models
{
    public class WeaponsDTO
    {
        public int Id { get; set; }
        public int WeaponType { get; set; }
        public decimal PriceOfBuy { get; set; }
        public string Characteristics { get; set; }
        public int CombatPower { get; set; }
    }
}
