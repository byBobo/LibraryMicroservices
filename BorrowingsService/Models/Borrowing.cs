using System;
using System.ComponentModel.DataAnnotations;

namespace BorrowingsService.Models
{
    public class Borrowing
    {
        [Key]
        public int Id { get; set; }
        public string idCustomer { get; set; }
        public string idBook { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
    }
}
