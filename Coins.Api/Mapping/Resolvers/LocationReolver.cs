using AutoMapper;
using Coins.Core.Models.Domins.Test;
using Coins.Core.Models.DtoAPI.Test;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Api.Mapping.Resolvers
{
    public class LocationReolver : IValueResolver<LocationTestDTO, LocationTest, Point>
    {

        public Point Resolve(LocationTestDTO source, LocationTest destination, Point destMember, ResolutionContext context)
        {
            if (source.Location != null)
                return new Point(source.Location.Lon, source.Location.Lat) { SRID = 4326 };
            else
                return null;
        }
    }
    public class LocationReverseReolver : IValueResolver<LocationTest, LocationTestDTO, LocationDTO>
    {

        public LocationDTO Resolve(LocationTest source, LocationTestDTO destination, LocationDTO destMember, ResolutionContext context)
        {
            if (source.Location != null)
                return new LocationDTO() { Lon = source.Location.X, Lat = source.Location.Y };
            else
                return null;
        }
    }
}
