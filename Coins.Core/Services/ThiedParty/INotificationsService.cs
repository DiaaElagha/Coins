using Coins.Core.Models.DtoAPI.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Services.ThiedParty
{
    public interface INotificationsService
    {
        public Task Send(string id, NotificationDto notifiObject, Dictionary<string, string> AdditionalData = null);
        public Task Send(IEnumerable<string> ids, NotificationDto notifiObject, Dictionary<string, string> AdditionalData = null);
        public Task SendToAll(NotificationDto notifiObject, Dictionary<string, string> AdditionalData = null);
    }
}
