using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.Models.Authentication
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Result { get; set; }
        public string Code { get; set; }
    }
}
