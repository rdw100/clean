using Bank.Application.Interfaces;
using Bank.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Mvc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            AccountViewModel model = _accountService.GetAccounts();

            return View(model);
        }
    }
}
