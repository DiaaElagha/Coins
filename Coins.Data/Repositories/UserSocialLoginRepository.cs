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
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Data.Repositories
{
    public class UserSocialLoginRepository : Repository<UserSocialLogin>, IUserSocialLoginRepository
    {
        public UserSocialLoginRepository(ApplicationDbContext context)
                : base(context)
        { }

        public void Update(UserSocialLogin userSocialLogin)
        {
            Context.Update(userSocialLogin);
        }

    }
}
