using System;
using System.ComponentModel.DataAnnotations;

namespace BookEater.Models
{
    public class ClubComment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime PostedDate { get; set; } = DateTime.Now;

        public string UserId { get; set; }
        public MyUser User { get; set; }

        public int ClubPickId { get; set; }
        public ClubPick ClubPick { get; set; }
    }
}