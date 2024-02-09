using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeCommApi.Models;

namespace CodeCommApi.Dto.Post.Request
{
    public class CreatePostDto
    {
         public String? PostTitle { get; set; }
        public PostType PostType { get; set; }
        public String? PostBody { get; set; }
    }
}