using BorrowingsService.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BorrowingsService.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Borrowing> Borrowings { get; set; }

    }
}
