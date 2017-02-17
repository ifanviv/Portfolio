using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCourseWorkApplication.Models
{
    public class ViewedUser
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int AnnouncementId { get; set; }
        public virtual Announcement Announement {get; set;}
        public virtual ApplicationUser User { get; set; }
    }
}