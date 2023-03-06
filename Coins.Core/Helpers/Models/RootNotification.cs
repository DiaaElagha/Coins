using System;
using System.Collections.Generic;
using System.Text;

namespace Coins.Core.Helpers.Models
{
    public class NotificationDTO
    {
        public string title { get; set; }
        public string body { get; set; }
    }

    public class DataNotification
    {
        public DataNotification()
        {
            click_action = "FLUTTER_NOTIFICATION_CLICK";
            screen = "OPEN_PAGE1";
            sound = "sound";
            status = "done";
        }
        public string title { get; set; }
        public string body { get; set; }
        public string click_action { get; set; }
        public string sound { get; set; }
        public string status { get; set; }
        public string screen { get; set; }
        public string extradata { get; set; }
        public int? orderId { get; set; }
        public string icon { get; set; }
    }

    public class RootNotification
    {
        public NotificationDTO notification { get; set; }
        public DataNotification data { get; set; }
        public string to { get; set; }
        public string priority => "high";
    }


}
