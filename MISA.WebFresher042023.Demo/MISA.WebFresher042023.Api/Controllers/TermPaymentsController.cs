using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Core.DTO.TermPayments;
using MISA.WebFresher042023.Core.Interfaces.Services;
using MISA.WebFresher042023.Core.Services;

namespace MISA.WebFresher042023.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TermPaymentsController : BaseController<TermPaymentDto, TermPaymentCreateDto, TermPaymentUpdateDto>
    {
        private readonly ITermPaymentService _termPaymentService;
        public TermPaymentsController(ITermPaymentService termPaymentService) : base(termPaymentService)
        {
            _termPaymentService = termPaymentService;
        }

        /// <summary>
        /// Tìm kiếm phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilter(int pageSize, int pageNumber, string? textSearch)
        {
            var res = await _termPaymentService.GetFilterAsync(pageSize, pageNumber, textSearch);
            return Ok(res);
        }
    }
}
