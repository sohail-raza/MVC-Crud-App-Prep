using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.NetCoreMVCCRUD.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCoreMVCCRUD.Controllers
{
    public class AccountController : Controller
    {
         
        [Route("Login")]
        public IActionResult Login([FromBody] Login loginInfo)
        {
            return Ok(loginInfo);
        }
    }
}
