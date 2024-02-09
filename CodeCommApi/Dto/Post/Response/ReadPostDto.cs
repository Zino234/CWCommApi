using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeCommApi.Models;

namespace CodeCommApi.Dto.Post.Response
{
    public class ReadPostDto
    {
        public Guid PostId { get; set; }
        public String? PostTitle { get; set; }
        public PostType PostType { get; set; }
        public String? PostBody { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public DateTime PostCreatedAt { get; set; }
        public DateTime PostUpdatedAt { get; set; }
        public bool PostIsDeleted { get; set; } = false;
    }
}
