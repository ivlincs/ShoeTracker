namespace ShoeTracker.Data.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Shoe
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Brand { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Model { get; set; } = null!;

        [Range(0, 10000)]
        public double TotalDistance { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        [Display(Name = "Shoe Category")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        //Nav Property
        public ICollection<Run> Runs { get; set; } = new List<Run>();

    }
}
