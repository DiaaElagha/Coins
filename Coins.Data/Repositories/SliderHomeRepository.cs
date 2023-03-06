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
    public class SliderHomeRepository : Repository<SliderHome>, ISliderHomeRepository
    {
        public SliderHomeRepository(ApplicationDbContext context)
                : base(context)
        { }

        public async override Task<IEnumerable<SliderHome>> GetAllAsync()
        {
            return await Context.SliderHome
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public void Update(SliderHome sliderHome)
        {
            Context.Update(sliderHome);
        }

    }
}
