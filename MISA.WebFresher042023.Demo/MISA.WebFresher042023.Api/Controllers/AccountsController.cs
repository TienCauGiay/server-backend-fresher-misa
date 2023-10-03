using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Core.DTO.Accounts;
using MISA.WebFresher042023.Core.Interfaces.Services;
using MISA.WebFresher042023.Core.Resources;
using MISA.WebFresher042023.Core.Services;
using MISA.WebFresher042023.Infrastructure.Common.Procedure;
using System.Net.Http.Headers;

namespace MISA.WebFresher042023.Api.Controllers
{
    /// <summary>
    /// Controller triển khai các phương thức của entities account
    /// </summary>
    /// Created By: BNTIEN (19/07/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<AccountDto, AccountCreateDto, AccountUpdateDto>
    {
        private readonly IAccountService _accountService;
        public AccountsController(IAccountService accountService) : base(accountService)
        {
            _accountService = accountService;
        }
        #region API riêng (Account)

        /// <summary>
        /// Tìm kiếm và phân trang trên giao diện
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns>Danh sách tài khoản theo tìm kiếm, phân trang</returns>
        /// Created By: BNTIEN (19/07/2023)
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilter(int pageSize, int pageNumber, string? textSearch)
        {
            var res = await _accountService.GetFilterAsync(pageSize, pageNumber, textSearch);
            return Ok(res);
        }

        /// <summary>
        /// Lấy danh sách tất cả các con của tài khoản có số tài khoản là tham số truyền vào
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (26/07/2023)
        [HttpGet("children")]
        public async Task<IActionResult> GetAllChildren(string parentId)
        {
            var res = await _accountService.GetAllChildrenAsync(parentId);
            return Ok(res);
        }

        /// <summary>
        /// Cập nhật trạng thái sử dụng của tài khoản
        /// </summary>
        /// <param name="account"></param>
        /// <param name="state"></param>
        /// <param name="isUpdateChildren"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (26/07/2023)
        [HttpPut("state")]
        public async Task<IActionResult> UpdateState(AccountUpdateDto account, int state, int isUpdateChildren)
        {
            var res = await _accountService.UpdateStateAsync(account, state, isUpdateChildren);
            return Ok(res);
        }

        /// <summary>
        /// Lấy danh sách tài khoản công nợ phải thu
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (26/07/2023)
        [HttpGet("recevable")]
        public async Task<IActionResult> GetReceivable(int pageSize, int pageNumber, string? textSearch)
        {
            var res = await _accountService.GetObjectAsync<AccountReceivable>(pageSize, pageNumber, textSearch, ProcConstantAccount.GET_RECEIVABLE);
            return Ok(res); 
        }

        /// <summary>
        /// Lây danh sách tài khoản công nợ phải trả
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (26/07/2023)
        [HttpGet("payable")]
        public async Task<IActionResult> GetPayableAsync(int pageSize, int pageNumber, string? textSearch)
        {
            var res = await _accountService.GetObjectAsync<AccountPayable>(pageSize, pageNumber, textSearch, ProcConstantAccount.GET_PAYABLE);
            return Ok(res);
        }

        /// <summary>
        /// Tìm kiếm tài khoản tổng hợp
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns>Danh sách tài khoản tổng hợp thỏa mãn</returns>
        /// Created By: BNTIEN (12/08/2023)
        [HttpGet("searchfilter")]
        public async Task<IActionResult> GetBySearchFilter(int pageSize, int pageNumber, string? textSearch)
        {
            var res = await _accountService.GetBySearchFilterAsync(pageSize, pageNumber, textSearch);
            return Ok(res);
        }
        /// <summary>
        /// Tìm kiếm tài khoản ở page danh sách tài khoản
        /// </summary>
        /// <param name="textSearch"></param>
        /// <returns>Danh sách tài khoản thỏa mãn</returns>
        /// Created By: BNTIEN (12/08/2023)
        [HttpGet("search")]
        public async Task<IActionResult> GetBySearch(int pageSize, int pageNumber, string? textSearch)
        {
            var res = await _accountService.GetBySearchAsync(pageSize, pageNumber, textSearch);
            return Ok(res);
        }

        /// <summary>
        /// Lấy danh sách tài khoản nợ
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (26/07/2023)
        [HttpGet("debt")]
        public async Task<IActionResult> GetDebt(int pageSize, int pageNumber, string? textSearch)
        {
            var res = await _accountService.GetObjectAsync<AccountDebt>(pageSize, pageNumber, textSearch, ProcConstantAccount.GET_DEBT);
            return Ok(res);
        }

        /// <summary>
        /// Lấy danh sách tài khoản có
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (26/07/2023)
        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance(int pageSize, int pageNumber, string? textSearch)
        {
            var res = await _accountService.GetObjectAsync<AccountBalance>(pageSize, pageNumber, textSearch, ProcConstantAccount.GET_BALANCE);
            return Ok(res);
        }

        /// <summary>
        /// Xử lí chức năng mở rộng trên giao diện
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (26/07/2023)
        [HttpGet("expand")]
        public async Task<IActionResult> GetExpand(int pageSize, int pageNumber, string? textSearch)
        {
            var res = await _accountService.GetExpandAsync(pageSize, pageNumber, textSearch);
            return Ok(res);
        }

        /// <summary>
        /// Xuất excel
        /// </summary>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (26/07/2023)
        [HttpGet("export")]
        public async Task<IActionResult> ExportExcel(string? textSearch)
        {
            MemoryStream memoryStream = await _accountService.ExportExcelAsync(textSearch);

            var contentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = AccountVN.Export_FileName,
            };
            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{AccountVN.Export_FileName}");
        }
        #endregion
    }
}
