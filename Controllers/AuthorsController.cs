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
        public async Task<IActionResult> GetAuthorByFilterAsync(AuthorFilterDTO authorFilterDTO)
        {
            var author = await _service.GetAuthorByFilterAsync(authorFilterDTO);
            return author == null ? NotFound() : Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorPostDTO author)
        {
            var result = await _service.AddAuthorAsync(author);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorEditDTO author)
        {
            if (id != author.Id) return BadRequest();
            var result = await _service.UpdateAuthorAsync(author);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            return Ok(await _service.DeleteAuthorAsync(id));
        }
    }
}
