namespace ShoeTracker.Data.Models.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Shoe Category")]
        public string Name { get; set; } = null!;

        //Nav propery
        public ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();
    }
}
