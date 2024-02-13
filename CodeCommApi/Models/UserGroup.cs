using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CodeCommApi.Models
{
    public class UserGroup
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        [NotMapped]
        public User? User { get; set; }

        public Guid GroupId { get; set; }
        [NotMapped]
        public Groups? Group { get; set; }

      
    }
}
