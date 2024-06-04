using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class DBContext : DbContext

    {

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {

        }

        public DbSet<Libros> Libros { get; set; }


        public List<Libros> GetAll()
        {

            return Libros.ToList();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Libros>().ToTable("Libro");

        }


    }



}
