using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Entities
{
    public class PersonalNotesDbContext : DbContext
    {
        public PersonalNotesDbContext(DbContextOptions options): base(options)
        {

        }
        public PersonalNotesDbContext()
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
