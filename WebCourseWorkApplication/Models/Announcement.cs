using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebCourseWorkApplication.Models
{
    public class Announcement
    {
        public int AnnouncementId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string ExtraDetails { get; set; }
        public string TimeStamp { get; set; }
        public string Author { get; set; }
        public string Search { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<ViewedUser> VUsers { get; set; }

    }
}