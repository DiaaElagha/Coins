using Coins.Core.Models.Domins.Auth;
using Coins.Entities.Domins.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Repositories
{
    public interface IUserLogsRepository : IRepository<UserLogs>
    {
        void Update(UserLogs userLog);

    }
}
