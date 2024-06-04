using DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace BOOKSTATION.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly DBContext _Context;

        public LibrosController(DBContext context)
        {
            _Context = context;

        }


        // GET: api/Libros
        [HttpGet("Lista")]
        public async Task<ActionResult<IEnumerable<Libros>>> GetLibros()
        {
            return await _Context.Libros.ToListAsync();
        }

        // GET: api/Libros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Libros>> GetLibro(int id)
        {
            var libro = await _Context.Libros.FindAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            return libro;
        }

        // POST: api/Libros
        [HttpPost]
        public async Task<ActionResult<Libros>> PostLibro(Libros libro)
        {
            _Context.Libros.Add(libro);
            await _Context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLibro), new { id = libro.LibroID }, libro);
        }


        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Libros request)
        {

            _Context.Libros.Update(request);
            await _Context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, "ok");

        }
        // DELETE: api/Libros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            var libro = await _Context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            _Context.Libros.Remove(libro);
            await _Context.SaveChangesAsync();

            return NoContent();
        }

        private bool LibroExists(int id)
        {
            return _Context.Libros.Any(e => e.LibroID == id);
        }
    }
}

