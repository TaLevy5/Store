using System.ComponentModel.DataAnnotations;
namespace Application.DTOs.Users
{
    public class UpdateUserDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set;} = string.Empty;
        [Required]
        [EmailAddress]
        [MaxLength(320)]
        public string Email { get; set;} = string.Empty;
        public bool? IsActive { get; set;}
    }
}