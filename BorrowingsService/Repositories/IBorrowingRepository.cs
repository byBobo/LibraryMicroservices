using BorrowingsService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BorrowingsService.Repositories
{
    public interface IBorrowingRepository
    {
        Task<IEnumerable<Borrowing>> ReadAllAsync();
        Task<Borrowing> ReadOneAsync(int id);
        Task<Borrowing> CreateAsync(Borrowing borrowing);
        Task<Borrowing> UpdateAsync(int id, Borrowing borrowing);
        Task<Borrowing> DeleteAsync(int id);
    }
}
