using Coins.Core;
using Coins.Core.Models.Domins.Test;
using Coins.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestService(IUnitOfWork unityOfWork)
        {
            _unitOfWork = unityOfWork;
        }
        public async Task<LocationTest> AddLocatinoTest(LocationTest locationTest)
        {
            await _unitOfWork.LocationTestRepository.AddAsync(locationTest);
            await _unitOfWork.CommitAsync();

            return locationTest;
        }

        public async Task<LocationTest> GetLocationTest(int id)
        {
            var test = _unitOfWork.LocationTestRepository.Find(t => t.ID == id).FirstOrDefault();
            return test;
        }

        public async Task<IEnumerable<LocationTest>> GetAllLocationtest()
        {
            var list = await _unitOfWork.LocationTestRepository.GetAllAsync();
            return list;
        }

        public IEnumerable<LocationTest> GetNearbyLocationTest(double x, double y, int distance)
        {
            var list = _unitOfWork.LocationTestRepository.GetNearby(x, y, distance);
            return list;
        }
    }
}
