namespace ShoeTracker.Data.Models.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Shoe
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Brand { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Model { get; set; } = null!;

        [Range(0.1, 10000)]
        public double TotalDistance { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        //Nav Property
        public ICollection<Run> Runs { get; set; } = new List<Run>();


    }
}
