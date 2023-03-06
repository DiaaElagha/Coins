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
    public class GeneralSettingsRepository : Repository<GeneralSettings>, IGeneralSettingsRepository
    {
        public GeneralSettingsRepository(ApplicationDbContext context)
                : base(context)
        { }

        public void Update(GeneralSettings generalSettings)
        {
            Context.Update(generalSettings);
        }


    }
}
