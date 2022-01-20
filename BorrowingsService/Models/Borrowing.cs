using System;
using System.ComponentModel.DataAnnotations;

namespace BorrowingsService.Models
{
    public class Borrowing
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public string BookId { get; set; }
        [Required]
        public DateTime FromData { get; set; }
        [Required]
        public DateTime ToData { get; set; }
    }
}
