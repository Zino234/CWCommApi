using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CodeCommApi.Models;

namespace CodeCommApi.Dto.DirectMessages.Requests
{
    public class CreateDirectMessageDto
    {
        [Required]
        public MessageType MessageType { get; set; }

        [Required]
        public string MessageBody { get; set; }

      
    }
}
