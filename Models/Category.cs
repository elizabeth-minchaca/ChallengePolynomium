using System.ComponentModel.DataAnnotations;

namespace ChallengePolynomius.Models
{
    public class Category
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
