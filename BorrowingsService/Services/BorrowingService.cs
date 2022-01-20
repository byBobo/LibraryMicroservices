using BorrowingsService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BorrowingsService.Repositories;

namespace BorrowingsService.Services
{
    public class BorrowingService : IBorrowingService
    {
        private IBorrowingRepository _borrowingRepository;

        public BorrowingService(IBorrowingRepository borrowingRepository)
        {
            _borrowingRepository = borrowingRepository ?? throw new ArgumentNullException(nameof(borrowingRepository));
        }
        public Task<IEnumerable<Borrowing>> ReadAllAsync()
        {
            return _borrowingRepository.ReadAllAsync();
        }

        public Task<Borrowing> ReadOneAsync(int id)
        {
            return _borrowingRepository.ReadOneAsync(id);
        }

        public Task<Borrowing> CreateAsync(Borrowing borrowing)
        {
            return _borrowingRepository.CreateAsync(borrowing);
        }
        public Task<Borrowing> UpdateAsync(int id, Borrowing borrowing)
        {
            return _borrowingRepository.UpdateAsync(id, borrowing);
        }

        public Task<Borrowing> DeleteAsync(int id)
        {
            return _borrowingRepository.DeleteAsync(id);
        }

    }
}
