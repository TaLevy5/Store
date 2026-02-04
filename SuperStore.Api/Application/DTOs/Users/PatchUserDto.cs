namespace Application.DTOs.Users
{
    public class PatchUserDto 
    {
        public string? Name { get; set;}
        public string? Email { get; set;}
        public bool? IsActive { get; set;}
    }
}