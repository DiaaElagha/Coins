using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.DtoAPI.User
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public String Message { get; set; }
        public bool IsRead { get; set; }
        public DateTimeOffset SendDateAt { get; set; }
        public bool Status { get; set; }

        public Guid? ReceverId { get; set; }
        public UserProfileDto ApplicationUserRecever { get; set; }

        public Guid? SenderId { get; set; }
        public UserProfileDto ApplicationUserSender { get; set; }
    }
}
