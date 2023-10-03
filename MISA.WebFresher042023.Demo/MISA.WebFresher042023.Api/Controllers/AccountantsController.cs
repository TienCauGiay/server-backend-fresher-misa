using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Core.DTO.Accountants;
using MISA.WebFresher042023.Core.Interfaces.Services;
using MISA.WebFresher042023.Core.Services;

namespace MISA.WebFresher042023.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountantsController : BaseController<AccountantDto, AccountantCreateDto, AccountantUpdateDto>
    {
        private readonly IAccountantService _accountantService;
        public AccountantsController(IAccountantService accountantService) : base(accountantService)
        {
            _accountantService = accountantService;
        }

        /// <summary>
        /// Lấy danh sách hạch toán theo phiếu chi
        /// </summary>
        /// <param name="receiptId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        [HttpGet("id/{receiptId}")]
        public async Task<IActionResult> GetByReceiptId(Guid receiptId)
        {
            var res = await _accountantService.GetByReceiptIdAsync(receiptId);
            return Ok(res);
        }
    }
}
