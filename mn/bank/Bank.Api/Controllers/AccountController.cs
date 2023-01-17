using Bank.Application.Interfaces;
using Bank.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AccountViewModel accountViewModel)
        {
            _accountService.Create(accountViewModel);

            return Ok(accountViewModel);
        }
    }
}