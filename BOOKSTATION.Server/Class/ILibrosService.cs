using DB;
using Microsoft.EntityFrameworkCore;

namespace BOOKSTATION.Server.Class   
{
   public interface ILibrosService
{
    Task<IEnumerable<Libros>> GetAllLibrosAsync();
    Task<Libros> GetLibroByIdAsync(int id);
    Task<Libros> CreateLibroAsync(Libros libro);
    Task<bool> UpdateLibroAsync(Libros libro);
    Task<bool> DeleteLibroAsync(int id);
}

public class LibrosService : ILibrosService
{
    public readonly DBContext _context;

    public LibrosService(DBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Libros>> GetAllLibrosAsync()
    {
        return await _context.Libros.ToListAsync();
    }

    public async Task<Libros> GetLibroByIdAsync(int id)
    {
        return await _context.Libros.FindAsync(id);
    }

    public async Task<Libros> CreateLibroAsync(Libros libro)
    {
        _context.Libros.Add(libro);
        await _context.SaveChangesAsync();
        return libro;
    }

    public async Task<bool> UpdateLibroAsync(Libros libro)
    {
        _context.Entry(libro).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LibroExists(libro.LibroID))
            {
                return false;
            }
            throw;
        }
        return true;
    }

    public async Task<bool> DeleteLibroAsync(int id)
    {
        var libro = await _context.Libros.FindAsync(id);
        if (libro == null)
        {
            return false;
        }

        _context.Libros.Remove(libro);
        await _context.SaveChangesAsync();
        return true;
    }

    private bool LibroExists(int id)
    {
        return _context.Libros.Any(e => e.LibroID == id);
    }
}
}
