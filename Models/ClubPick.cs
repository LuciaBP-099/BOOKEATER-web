using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookEater.Models
{
    public class ClubPick
    {
        public int Id { get; set; }

        public DateTime Month { get; set; } = DateTime.Now;

        public string? Topic { get; set; }

        public int BookId { get; set; }
        public Book? Book { get; set; }

        public ICollection<ClubComment> Comments { get; set; } = new List<ClubComment>();
    }
}