namespace ShoeTracker.Data.Models.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static ShoeTracker.Common.ValidationConstants.UserProfile;

    public class UserProfile
    {
        [Key]
        public string UserId { get; set; } = null!;

        [MaxLength(CityMaxLength,ErrorMessage ="City name cannot exceed 100 characters.")]
        [Display(Name = "City")]
        public string? City { get; set; }

        [MaxLength(BioMaxLength,ErrorMessage ="Bio cannot exceed 500 characters.")]
        [Display(Name = "About me")]
        public string? Bio { get; set; }

        [Display(Name = "Member since")]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Range(0, 10000,ErrorMessage ="Yearly goal must be between 0 and 10000km.")]
        [Display(Name = "Yearly distance goal (km)")]
        public double YearlyGoal { get; set; } = 0;

    }
}