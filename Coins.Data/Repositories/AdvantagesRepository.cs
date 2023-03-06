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
    public class AdvantagesRepository : Repository<Advantages>, IAdvantagesRepository
    {
        public AdvantagesRepository(ApplicationDbContext context)
                : base(context)
        { }

        public async override Task<IEnumerable<Advantages>> GetAllAsync()
        {
            return await Context.Advantages
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public void Update(Advantages advantage)
        {
            Context.Update(advantage);
        }

    }
}
