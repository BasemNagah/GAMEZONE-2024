namespace GAMEZONE.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
