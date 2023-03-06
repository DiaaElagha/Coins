using Coins.Core.Models.Domins.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Services
{
    public interface ITestService
    {
        Task<LocationTest> AddLocatinoTest(LocationTest locationTest);
        Task<LocationTest> GetLocationTest(int id);
        Task<IEnumerable<LocationTest>> GetAllLocationtest();

        IEnumerable<LocationTest> GetNearbyLocationTest(double x, double y, int distance);
    }
}
