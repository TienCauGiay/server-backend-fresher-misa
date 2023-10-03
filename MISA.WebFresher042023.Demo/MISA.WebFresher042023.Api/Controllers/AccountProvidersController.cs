using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Core.DTO.AccountProviders;
using MISA.WebFresher042023.Core.Interfaces.Services;

namespace MISA.WebFresher042023.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountProvidersController : BaseController<AccountProviderDto, AccountProviderCreateDto, AccountProviderUpdateDto>
    {
        private readonly IAccountProviderService _accountProviderService;
        public AccountProvidersController(IAccountProviderService accountProviderService) : base(accountProviderService)
        {
            _accountProviderService = accountProviderService;
        }

        /// <summary>
        /// Lấy danh sách tài khoản ngân hàng theo id provider
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        [HttpGet("id/{providerId}")]
        public async Task<IActionResult> GetByProviderId(Guid providerId)
        {
            var res = await _accountProviderService.GetByProviderIdAsync(providerId);
            return Ok(res);
        }
    }
}
