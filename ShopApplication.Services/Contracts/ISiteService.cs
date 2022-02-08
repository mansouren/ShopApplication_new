using ShopApplication.DataLayer.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApplication.Services
{
    public interface ISiteService
    {
        Task<Settings> GetSiteSetting();
        Task UpdateSettings(Settings settings,CancellationToken cancellationToken);
        Task UpdateCallSettings(Settings settings, CancellationToken cancellationToken);
        Task UpdateSmsSettings(Settings settings, CancellationToken cancellationToken);
    }
}