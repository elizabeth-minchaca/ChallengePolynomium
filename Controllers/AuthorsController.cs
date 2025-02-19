using ChallengePolynomius.DTOs;
using ChallengePolynomius.Models;
using ChallengePolynomius.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace ChallengePolynomius.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;
        public AuthorsController(IAuthorService service) { _service = service; }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var authors = await _service.GetAuthorsList();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _service.GetAuthorByIdAsync(id);
            return author != null ? Ok(author) : NotFound(new { Message = "Autor no encontrado" });
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetAuthorByFilterAsync([FromQuery] AuthorFilterDTO authorFilterDTO)
        {
            var author = await _service.GetAuthorByFilterAsync(authorFilterDTO);
            return author == null ? NotFound("Autor no encontrado") : Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorPostDTO author)
        {
            if (author == null)
                return BadRequest("Datos del autor inválidos");

            try
            {
                var result = await _service.AddAuthorAsync(author);
                return StatusCode(201, result); //201 Created
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al agregar el autor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorEditDTO author)
        {
            if (id != author.Id) return BadRequest("El ID del autor no coincide con el del cuerpo de la solicitud");

            try
            {
                var result = await _service.UpdateAuthorAsync(author);
                return result != null
                    ? Ok(new { Message = "Autor actualizado", Data = result })
                    : NotFound($"No se encontró el autor con ID: {id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al actualizar el autor", Error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var success = await _service.DeleteAuthorAsync(id);

                if (!success)
                {
                    return NotFound("Autor no encontrado.");
                }

                return Ok("Autor eliminado con éxito.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al intentar eliminar el autor.", Error = ex.Message });
            }
        }
    }
}
