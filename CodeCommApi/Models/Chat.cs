using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CodeCommApi.Models
{
    [PrimaryKey("UserID1","UserID2")]
    public class Chat
    {
        [Key]
        public Guid ChatId { get; set; }
        [ForeignKey("User1")]
        public Guid UserID1 { get; set; }
        public User User1 { get; set; }
        [ForeignKey("User2")]
        public Guid UserID2 { get; set; }
        public User User2 { get; set; }
    }
}