using Coins.Core.Models.Domins.Auth;
using Coins.Entities.Domins.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Repositories
{
    public interface INotificationsRepository : IRepository<Notifications>
    {
        void Update(Notifications notification);
    }
}
