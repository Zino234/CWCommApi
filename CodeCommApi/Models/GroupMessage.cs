using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CodeCommApi.Models
{
    public class GroupMessage
    {
        [Key]
        public Guid MessageId { get; set; }
        public MessageType MessageType { get; set; }=MessageType.Text;
       [Required]
        public  String MessageBody { get; set; }
        

        //GROUP ID FOR THE GROUP WHICH THE MESSAGE IS GOING TO BE SENT INTO
        [Required]
        // [ForeignKey("Group")]
        public Guid GroupId { get; init; }
        // public Groups Group {get;set;}

        //USER ID FOR THE SENDER OF THE  MESSAGE
        [Required]
        public Guid MessageSentBy { get; set; }
        public DateTime MessageTimeSent { get; init; }=DateTime.Now;
        public DateTime? EditedAt { get; set; }=null;
        public DeletedMessageType MessageIsDeleted { get; set; }=DeletedMessageType.NotDeleted;

    }
}