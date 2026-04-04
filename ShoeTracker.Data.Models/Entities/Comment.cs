namespace ShoeTracker.Data.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static ShoeTracker.Common.ValidationConstants.Comment;

    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Comment content is required")]
        [MaxLength(ContentMaxLength, ErrorMessage = "Comment must be between 5 and 500 characters")]
        [Display(Name = "Comment")]
        public string Content { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        [ForeignKey(nameof(Shoe))]
        public int ShoeId { get; set; }

        public Shoe Shoe { get; set; } = null!;

        [Required]
        public string UserId { get; set; } = null!;
    }
}
