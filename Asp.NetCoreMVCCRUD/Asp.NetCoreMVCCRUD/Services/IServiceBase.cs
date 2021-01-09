using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Services
{
    public interface IServiceBase
    {
        Task<bool> CompleteUnitOfWork();
    }
}
