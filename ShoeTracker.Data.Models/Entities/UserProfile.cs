namespace ShoeTracker.Data.Models.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static ShoeTracker.Common.ValidationConstants.UserProfile;

    public class UserProfile
    {
        [Key]
        public string UserId { get; set; } = null!;

        [MaxLength(CityMaxLength)]
        [Display(Name = "City")]
        public string? City { get; set; }

        [MaxLength(BioMaxLength)]
        [Display(Name = "About me")]
        public string? Bio { get; set; }

        [Display(Name = "Member since")]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Range(0, 10000)]
        [Display(Name = "Yearly distance goal (km)")]
        public double YearlyGoal { get; set; } = 0;

    }
}