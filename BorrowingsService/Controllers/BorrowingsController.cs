using BorrowingsService.Data;
using BorrowingsService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BorrowingsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BorrowingsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var borrowings = await _context.Borrowings.ToListAsync();

            return new JsonResult(borrowings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var borrowing = await _context.Borrowings.FirstOrDefaultAsync(b => b.Id == id);

            return new JsonResult(borrowing);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Borrowing borrowing)
        {
            _context.Borrowings.Add(borrowing);
            await _context.SaveChangesAsync();

            return new JsonResult(borrowing.Id);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, Borrowing borrowing)
        {
            var existingBorrowing = await _context.Borrowings.FirstOrDefaultAsync(b => b.Id == id);
            existingBorrowing.idCustomer = borrowing.idCustomer;
            existingBorrowing.idBook = borrowing.idBook;
            existingBorrowing.from = borrowing.from;
            existingBorrowing.to = borrowing.to;

            var success = (await _context.SaveChangesAsync()) > 0;

            return new JsonResult(success);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var borrowing = await _context.Borrowings.FirstOrDefaultAsync(b => b.Id == id);
            _context.Remove(borrowing);
            var success = (await _context.SaveChangesAsync()) > 0;

            return new JsonResult(success);
        }
    }
}
