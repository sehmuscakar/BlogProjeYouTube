using CoreLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{//makale
    public class Article:BaseEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }
        public int viewCount { get; set; } = 0;
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid? ImageId { get; set; } //userlarda bı sıkıntı yoko ama burda var ımageıd kısmıında 
        public Image Image { get; set; }

        public Guid UserId { get; set; }
        public AppUser User{ get; set; }

    }
}
