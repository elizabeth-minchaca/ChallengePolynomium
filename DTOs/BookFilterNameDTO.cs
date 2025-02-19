namespace ChallengePolynomius.DTOs
{
    public class BookFilterNameDTO
    {
        public int? Id { get; set; }              // Filtro por ID (opcional)
        public string Title { get; set; }          // Filtro por título (opcional)
        public string? AuthorName { get; set; }     // Filtro por nombre del autor (opcional)
        public string? CategoryName { get; set; }   // Filtro por nombre de la categoría (opcional)

        // Paginación
        public int Page { get; set; } = 1;         // Página actual
        public int PageSize { get; set; } = 10;    // Tamaño de la página
    }
}
