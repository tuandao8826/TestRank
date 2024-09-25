using System.ComponentModel.DataAnnotations;

namespace TestRank.Entities
{
    public class Score
    {
        [Key]
        public int UserId { get; set; }
        public User User { get; set; }

        [Key]
        public DateTime PlayDay { get; set; }

        public int Point { get; set; }
    }
}
