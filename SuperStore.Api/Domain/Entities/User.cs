namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set;}
        public string Name { get; set;} = string.Empty;
        public string Email { get; set;} = string.Empty;
        public bool IsActive { get; set;} = true;
        public DateTime CreatedAt { get; set;} = DateTime.UtcNow;

        // add it after creating Order entity
        // public ICollection<Order> Orders { get; set;} = new List<Order>();

    }
}