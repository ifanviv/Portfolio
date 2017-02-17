using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebCourseWorkApplication.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        [Display(Name="Comment")]
        public string Description { get; set; }
        public string Author { get; set; }

        public int AnnouncementId { get; set; }
        public virtual Announcement Announcement { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}