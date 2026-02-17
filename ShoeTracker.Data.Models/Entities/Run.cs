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
        public double Distance { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [ForeignKey(nameof(Shoe))]
        public int ShoeId { get; set; }

        public Shoe Shoe { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;
    }
}
