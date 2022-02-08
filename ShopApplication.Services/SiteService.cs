
using Microsoft.EntityFrameworkCore;
using ShopApplication.Common;
using ShopApplication.DataLayer.Entities;
using ShopApplication.DataLayer.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopApplication.Services
{
   public class SiteService : ISiteService,IScopeDependency
    {
        private readonly IRepository<Settings> repository;
        

        public SiteService(IRepository<Settings> repository)
        {
            this.repository = repository;
            
        }
        public async Task<Settings> GetSiteSetting()
        {
           return await repository.TableAsNoTracking.FirstOrDefaultAsync();
        }

        public async Task UpdateCallSettings(Settings settings, CancellationToken cancellationToken)
        {
            var model = await GetSiteSetting();
            model.Address = settings.Address;
            model.Telephone = settings.Telephone;
            model.Mobile = settings.Mobile;
            model.Fax = settings.Fax;
            await repository.UpdateAsync(model, cancellationToken);
        }

        public async Task UpdateSettings(Settings settings, CancellationToken cancellationToken)
        {
            var set =await GetSiteSetting();
            set.Name = settings.Name;
            set.Description = settings.Description;
            set.KeyWords = settings.KeyWords;
            await repository.UpdateAsync(set, cancellationToken);
            
        }

        public async Task UpdateSmsSettings(Settings settings, CancellationToken cancellationToken)
        {
            var set = await GetSiteSetting();
            set.Mobile = settings.Mobile;
            set.SmsServiceUserName = settings.SmsServiceUserName;
            set.SmsServicePassword = settings.SmsServicePassword;
            set.SmsServiceNumber = settings.SmsServiceNumber;
            set.PayIsSend = settings.PayIsSend;
            set.FactorIsSend = settings.FactorIsSend;
            await repository.UpdateAsync(set, cancellationToken);

        }
    }
}
