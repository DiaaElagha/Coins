using Coins.Core.Models.Domins.Test;
using Coins.Core.Repositories;
using Coins.Data.Data;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Data.Repositories
{
    public class LocationTestRepository : Repository<LocationTest>, ILocationTestRepositiry
    {
        public LocationTestRepository(ApplicationDbContext context)
                   : base(context)
        { }

        public IEnumerable<LocationTest> GetNearby(double x, double y, int distance)
        {
            Point somePoint = new Point(x, y);
            var tran = new NetTopologySuite.Geometries.Utilities.GeometryTransformer();
            //NetTopologySuite.Operation.Distance.DistanceOp.IsWithinDistance(,,)
            //var nearBy = Context.LocationTests.Where(c => c.Location.Distance(somePoint) < distance).ToList();
            var nearBy = Context.LocationTests.Where(c => NetTopologySuite.Operation.Distance.DistanceOp.IsWithinDistance(c.Location, somePoint, 100)).ToList();

            return nearBy;
            //return null;
        }
    }
}
