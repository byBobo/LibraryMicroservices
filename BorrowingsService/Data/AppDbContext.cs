using BorrowingsService.Models;
using Microsoft.EntityFrameworkCore;

namespace BorrowingsService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Borrowing> Borrowings { get; set; }
    }
}
