namespace ShoeTracker.Data.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static ShoeTracker.Common.ValidationConstants.Run;

    public class Run
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Distance is required")]
        [Range(MinDistance,MaxDistance,ErrorMessage = "Distance must be between 0.1 and 100km.")]
        [Display(Name = "Running mileage")]
        public double Distance { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Required]
        [ForeignKey(nameof(Shoe))]
        public int ShoeId { get; set; }

        public Shoe Shoe { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;
    }
}
