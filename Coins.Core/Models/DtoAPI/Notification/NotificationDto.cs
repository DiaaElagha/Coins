using Coins.Core.Constants.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.DtoAPI.Notification
{
    public class NotificationDto
    {
        public string TitleAr { get; set; }
        public string TitleEn { get; set; }
        public string BodyAr { get; set; }
        public string BodyEn { get; set; }
        public NotificationType NotificationType { get; set; }

    }
}
