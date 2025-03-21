

namespace DuoWord.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string State { get; set; }
    }
}
