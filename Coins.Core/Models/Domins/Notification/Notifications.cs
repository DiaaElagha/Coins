using Coins.Core.Constants.Enums;
using Coins.Entities.Domins.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Coins.Entities.Domins.Notification
{
    public class Notifications
    {
        public Notifications()
        {
            SendDateAt = DateTimeOffset.Now;
            IsRead = false;
            Status = false;
        }
        [Key]
        public int Id { get; set; }
        public String Title { get; set; }
        public String Message { get; set; }
        public bool IsRead { get; set; }
        public DateTimeOffset SendDateAt { get; set; }
        public bool Status { get; set; }
        public NotificationType NotificationType { get; set; }

        public Guid? ReceverId { get; set; }
        [ForeignKey(nameof(ReceverId))]
        public ApplicationUser ApplicationUserRecever { get; set; }

        public Guid? SenderId { get; set; }
        [ForeignKey(nameof(SenderId))]
        public ApplicationUser ApplicationUserSender { get; set; }
    }
}
