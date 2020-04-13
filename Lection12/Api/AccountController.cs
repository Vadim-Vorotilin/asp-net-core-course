using Microsoft.AspNetCore.Mvc;

namespace Api
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

        [HttpPost("[action]")]
        public IActionResult CreateAccount()
        {
            return Ok(_accountService.CreateAccount());
        }

        [HttpPatch("[action]")]
        public IActionResult Debit([FromQuery] string accountId,
                                   [FromQuery] double amount)
        {
            _accountService.Debit(accountId, amount);

            return Ok(_accountService.GetBalance(accountId));
        }
    }
}