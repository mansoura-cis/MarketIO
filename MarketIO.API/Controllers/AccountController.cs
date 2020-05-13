using MarketIO.API.Auth;
using MarketIO.Contracts.V1;
using MarketIO.Contracts.V1.Requests;
using MarketIO.DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MarketIO.API.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _account;
        private readonly IJwtAuthHandler _jwt;
        public AccountController(IAccountRepository account , IJwtAuthHandler jwt)
        {
            _account = account;
            _jwt = jwt;
        }


        [AllowAnonymous]
        [HttpPost(APIRoutes.Account.Login)]
        public async Task<IActionResult> Login([FromForm]LoginViewModel model)
        {
           var result = await _account.CheckAuthority(model.Email, model.Password);
            if (result.Item1 != null)
            {
                return Ok($"Bearer { _jwt.CreateToken(result.Item1, result.Item2.FirstOrDefault())} {DateTime.Now} ");
            }
            return Unauthorized("You are not Authorized");
        }

        
        [HttpGet(APIRoutes.Account.CheckLogin)]
        public IActionResult CheckLogin() => Ok("I am Authorized");
        
        
    }
}
