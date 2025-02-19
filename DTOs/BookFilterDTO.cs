namespace ChallengePolynomius.DTOs
{
    public class BookFilterDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public int? AuthorId { get; set; }
        public int? CategoryId { get; set; }
    }
}
