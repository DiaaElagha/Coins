using Coins.Core.Models.Domins.Auth;
using Coins.Core.Models.Domins.Home;
using Coins.Core.Models.Domins.Social;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Core.Repositories;
using Coins.Data.Data;
using Coins.Data.Repositories;
using Coins.Entities.Domins.Auth;

using Coins.Entities.Domins.Notification;
using Coins.Entities.Domins.StoresInfo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Data.Repositories
{
    public class UserStoresRepository : Repository<UserStores>, IUserStoresRepository
    {
        public UserStoresRepository(ApplicationDbContext context)
                : base(context)
        { }

        public void Update(UserStores userStore)
        {
            Context.Update(userStore);
        }
    }
}
