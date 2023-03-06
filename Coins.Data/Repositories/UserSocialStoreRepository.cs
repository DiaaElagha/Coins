using Coins.Core.Models.Domins.Auth;
using Coins.Core.Models.Domins.Home;
using Coins.Core.Models.Domins.Social;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Core.Repositories;
using Coins.Data.Data;
using Coins.Data.Repositories;

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
    public class UserSocialStoreRepository : Repository<UserSocialStore>, IUserSocialStoreRepository
    {
        public UserSocialStoreRepository(ApplicationDbContext context)
                : base(context)
        { }

        public void Update(UserSocialStore userSocialStore)
        {
            Context.Update(userSocialStore);
        }
    }
}
