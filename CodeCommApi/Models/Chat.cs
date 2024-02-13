using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CodeCommApi.Models
{
    [PrimaryKey("UserID1", "UserID2")]
    public class Chat
    {
        public Guid ChatId { get; set; }

        //NAVIGATION PROPERTIES FOR THE USER 1
        public Guid UserID1 { get; set; }

        [NotMapped]
        public User? User1 { get; set; }

        //NAVIGATION PROPERTIES FOR THE USER 2
        public Guid UserID2 { get; set; }

        [NotMapped]
        public User? User2 { get; set; }
    }
}
