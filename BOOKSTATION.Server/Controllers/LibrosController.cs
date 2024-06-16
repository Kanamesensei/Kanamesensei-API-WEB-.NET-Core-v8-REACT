using BOOKSTATION.Server.Class;
using DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BOOKSTATION.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        public readonly ILibrosService _librosService;

        public LibrosController(ILibrosService librosService)
        {
            _librosService = librosService;
        }

        // GET: api/Libros/Lista
        [HttpGet("Lista")]
        public async Task<ActionResult<IEnumerable<Libros>>> GetLibros()
        {
            var libros = await _librosService.GetAllLibrosAsync();
            return Ok(libros);
        }

        // GET: api/Libros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Libros>> GetLibro(int id)
        {
            var libro = await _librosService.GetLibroByIdAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            return Ok(libro);
        }

        // POST: api/Libros
        [HttpPost]
        public async Task<ActionResult<Libros>> PostLibro(Libros libro)
        {
            var createdLibro = await _librosService.CreateLibroAsync(libro);
            return CreatedAtAction(nameof(GetLibro), new { id = createdLibro.LibroID }, createdLibro);
        }

        // PUT: api/Libros/Editar
        [HttpPut("Editar")]
        public async Task<IActionResult> Editar(Libros libro)
        {
            var updated = await _librosService.UpdateLibroAsync(libro);
            if (!updated)
            {
                return NotFound();
            }
            return Ok("Libro actualizado correctamente");
        }

        // DELETE: api/Libros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            var deleted = await _librosService.DeleteLibroAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
