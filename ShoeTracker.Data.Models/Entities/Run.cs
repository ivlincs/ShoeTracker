namespace ShoeTracker.Data.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static ShoeTracker.Common.ValidationConstants.Run;

    public class Run
    {
        public int Id { get; set; }

        [Required]
        [Range(MinDistance,MaxDistance)]
        [Display(Name = "Running mileage")]
        public double Distance { get; set; }

        [Required]
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
