using Coins.Core.Models.Domins.Home;

namespace Coins.Core.Repositories
{
    public interface ISliderHomeRepository : IRepository<SliderHome>
    {
        void Update(SliderHome sliderHome);

    }
}
