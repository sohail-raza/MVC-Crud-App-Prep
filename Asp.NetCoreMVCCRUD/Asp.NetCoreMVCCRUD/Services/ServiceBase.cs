using Asp.NetCoreMVCCRUD.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Services
{
    public class ServiceBase<T> : IServiceBase where T : class
    {
        private readonly CompanyDatabaseContext _dbContext;
        protected readonly ILogger<T> _logger;

        public ServiceBase(CompanyDatabaseContext dbContext, ILogger<T> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException($"[{nameof(ServiceBase<T>)}.Constructor] : {nameof(dbContext)} was null");
            _logger = logger ?? throw new ArgumentNullException($"[{nameof(Logger<T>)}.Constructor] : {nameof(Logger<T>)} was null");
        }

        public async Task<bool> CompleteUnitOfWork()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("[SaveChangesAsync] - Exception saving changes to database. Exception: {exception}", ex);
                return false;
            }
        }
    }
}
