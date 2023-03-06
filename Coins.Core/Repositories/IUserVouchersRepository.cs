using Coins.Core.Models.Domins.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Repositories
{
    public interface IUserVouchersRepository : IRepository<UserVouchers>
    {
        void Update(UserVouchers userVoucher);

    }
}
