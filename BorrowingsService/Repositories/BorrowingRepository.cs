using BorrowingsService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BorrowingsService.Repositories
{
    public class BorrowingRepository : IBorrowingRepository
    {
        protected readonly AppDbContext _context;

        public BorrowingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Borrowing>> ReadAllAsync()
        {
            return await _context.Borrowings.ToListAsync();
        }

        public async Task<Borrowing> ReadOneAsync(int id)
        {
            var result = await _context.Borrowings.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<Borrowing> CreateAsync(Borrowing borrowing)
        {
            await _context.Borrowings.AddAsync(borrowing);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<Borrowing> UpdateAsync(int id, Borrowing borrowing)
        {
            var result = await _context.Borrowings.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                result.BookId = borrowing.BookId;
                result.CustomerId = borrowing.CustomerId;
                result.FromData = borrowing.FromData;
                result.ToData = borrowing.ToData;

                await _context.SaveChangesAsync();
                return result;
            }
            return null;  
        }

        public async Task<Borrowing> DeleteAsync(int id)
        {
            var result = await _context.Borrowings.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                _context.Borrowings.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;

        }

    }
} 
