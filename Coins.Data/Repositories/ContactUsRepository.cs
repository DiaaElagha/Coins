using Coins.Core.Models.Domins.Home;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Core.Repositories;
using Coins.Data.Data;
using Coins.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Data.Repositories
{
    public class ContactUsRepository : Repository<ContactUs>, IContactUsRepository
    {
        public ContactUsRepository(ApplicationDbContext context)
                : base(context)
        { }

        public async override Task<IEnumerable<ContactUs>> GetAllAsync()
        {
            return await Context.ContactUs
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }



    }
}
