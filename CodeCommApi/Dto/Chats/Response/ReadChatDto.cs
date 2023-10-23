using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeCommApi.Models;

namespace CodeCommApi.Dto.Chats.Response
{
    public class ReadChatDto
    {
        public Guid ChatId { get; set; }

        public User User1 { get; set; }

        public User User2 { get; set; }

    }
}