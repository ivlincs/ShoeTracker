namespace ShoeTracker.Data.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static ShoeTracker.Common.ValidationConstants.Shoe;

    public class Shoe
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(BrandMaxLength)]
        [Display(Name = "Brand")]
        public string Brand { get; set; } = null!;

        [Required]
        [MaxLength(ModelMaxLength)]
        [Display(Name = "Model")]
        public string Model { get; set; } = null!;

        [Range(MinDistance,MaxDistance)]
        [Display(Name = "Total Distance")]
        public double TotalDistance { get; set; }

        [Required]
        [Display(Name = "Purchase date")]
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
