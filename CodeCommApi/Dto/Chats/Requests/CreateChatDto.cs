using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using CodeCommApi.Models;

namespace CodeCommApi.Dto.Chats.Requests
{
    public class CreateChatDto
    {

        //public Guid ChatId { get; set; }
        public Guid UserID1 { get; set; } 
        public Guid UserID2 { get; set; }

    }
}