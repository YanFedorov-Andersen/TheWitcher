namespace TheWitcher.Domain.Models
{
    public class ClothesDTO
    {
        public int Id { get; set; }
        public int ClothesType { get; set; }
        public decimal PriceOfBuy { get; set; }
        public string Characteristics { get; set; }
        public string Colour { get; set; }
        public int CombatPower { get; set; }
    }
}
