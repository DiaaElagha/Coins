using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace Coins.Core.Models.Domins.Test
{
    public class LocationTest
    {
        public int ID { get; set; }

        [Column(TypeName = "geometry")]
        public Point Location { get; set; }
    }
}
