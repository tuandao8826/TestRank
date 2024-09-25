using System.ComponentModel.DataAnnotations;

namespace TestRank.Entities
{
    public class User : BaseEntity<int>
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string? Image { get; set; }
    }
}
