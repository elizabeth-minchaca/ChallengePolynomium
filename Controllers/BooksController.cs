using ChallengePolynomius.DTOs;
using ChallengePolynomius.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChallengePolynomius.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // Obtener lista de libros con filtros
        /*[HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] BookFilterNameDTO filter)
        {
            var books = await _bookService.GetBooksAsync(filter);
            return Ok(books);
        }*/

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetBooksList();
            return books.Any() ? Ok(books) : NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            return book == null ? NotFound("Libro no encontrado") : Ok(book);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetBookByFilter([FromQuery] BookFilterDTO bookFilter)
        {
            var book = await _bookService.GetBookByFilterAsync(bookFilter);
            return book == null ? NotFound("Libro no encontrado") : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookPostDTO bookPostDTO)
        {
            var result = await _bookService.AddBookAsync(bookPostDTO);
            return CreatedAtAction(nameof(GetBookById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookEditDTO bookEditDTO)
        {
            if (id != bookEditDTO.Id) return BadRequest("El ID del libro no coincide con el del cuerpo de la solicitud");

            var result = await _bookService.UpdateBookAsync(bookEditDTO);
            return result != null ? Ok(result) : NotFound("Libro no encontrado");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var success = await _bookService.DeleteBookAsync(id);
            return success ? NoContent() : NotFound("Libro no encontrado");
        }
    }
}
