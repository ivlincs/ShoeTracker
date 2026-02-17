namespace ShoeTracker.Data.Models.Entities
{
    using System.ComponentModel.DataAnnotations;

    using static ShoeTracker.Common.ValidationConstants.Category;

    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        [Display(Name = "Shoe Category")]
        public string Name { get; set; } = null!;

        //Nav propery
        public ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();
    }
}
