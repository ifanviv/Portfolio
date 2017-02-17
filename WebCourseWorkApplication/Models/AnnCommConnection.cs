using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCourseWorkApplication.Models
{
    public class AnnCommConnection
    {
        public int Id { get; set; }
        public virtual Comment Comment { get; set; }
        public virtual ICollection<Announcement> Announcement { get; set; }
    }
}