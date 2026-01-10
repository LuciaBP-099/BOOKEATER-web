using System;
using System.ComponentModel.DataAnnotations;

namespace BookEater.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        [StringLength(1000)]
        public string? Content { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        public DateTime DatePosted { get; set; } = DateTime.Now;

        public int BookId { get; set; }
        public Book? Book { get; set; }

        public string? UserId { get; set; }
        public MyUser? User { get; set; }
    }
}