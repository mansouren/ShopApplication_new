using ShopApplication.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApplication.DataLayer.Repositories.Contracts
{
    public interface ISliderRepository : IRepository<Slider>
    {
        Task AddSlider(Slider slider, CancellationToken cancellationToken);
        Task UpdateSlider(Slider slider, CancellationToken cancellationToken);
        Task DeleteSlider(Slider slider, CancellationToken cancellationToken);
    }
}
