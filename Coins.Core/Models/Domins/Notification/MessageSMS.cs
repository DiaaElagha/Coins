using Coins.Entities.Domins.Auth;
using Coins.Entities.Domins.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Coins.Entities.Domins.Notification
{
    public class MessageSMS : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public String Title { get; set; }
        public String Message { get; set; }

        public Guid? ReceverId { get; set; }
        [ForeignKey(nameof(ReceverId))]
        public ApplicationUser ApplicationUserRecever { get; set; }
    }
}
