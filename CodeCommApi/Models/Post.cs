using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CodeCommApi.Models
{
    public class Post
    {
        [Key]
        public Guid PostId { get; set; }
        public String? PostTitle { get; set; }
        public PostType PostType { get; set; }
        public String? PostBody { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public DateTime PostCreatedAt { get; set; }=DateTime.Now;
        public DateTime PostUpdatedAt { get; set; }=DateTime.MinValue;
        public bool PostIsDeleted { get; set; }=false;
    }

    public class Job : Post
    {
        public PostType PostType { get; set; } = PostType.Job;
    }
}
