using System.ComponentModel.DataAnnotations;
namespace Application.DTOs.Users
{
    public class PatchUserDto 
    {
        [StringLength(100, MinimumLength = 2)]
        public string? Name { get; set;}
        [EmailAddress]
        [StringLength(320)]
        public string? Email { get; set;}
        public bool? IsActive { get; set;}
    }
}