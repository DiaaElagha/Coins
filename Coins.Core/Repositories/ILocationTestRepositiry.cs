using Coins.Core.Models.Domins.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Repositories
{
    public interface ILocationTestRepositiry : IRepository<LocationTest>
    {
        IEnumerable<LocationTest> GetNearby(double x, double y, int distance);
    }
}
