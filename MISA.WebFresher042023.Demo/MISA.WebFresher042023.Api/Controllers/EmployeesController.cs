using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Core.DTO.Employees;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Interfaces.Services;
using System.Net.Http.Headers;
using MISA.WebFresher042023.Core;
using MISA.WebFresher042023.Core.Resources;

namespace MISA.WebFresher042023.Api.Controllers
{
    /// <summary>
    /// Controller triển khai các phương thức của entities employee
    /// </summary>
    /// Created By: BNTIEN (17/06/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }

        #region API riêng (Employee)
        /// <summary>
        /// Tìm kiếm, phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns>Danh sách employee theo phân trang, tìm kiếm</returns>
        /// Created By: BNTIEN (17/06/2023)
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilter(int pageSize, int pageNumber, string? textSearch)
        {
            var res = await _employeeService.GetFilterAsync(pageSize, pageNumber, textSearch);
            return Ok(res);
        }

        /// <summary>
        /// Lấy mã nhân viên lớn nhất trong hệ thống
        /// </summary>
        /// <returns>mã nhân viên lớn nhất (nếu có)</returns>
        /// Created By: BNTIEN (17/06/2023)
        [HttpGet("maxcode")]
        public async Task<IActionResult> GetByCodeMax()
        {
            var res = await _employeeService.GetByCodeMaxAsync();
            return Ok(res);
        }

        /// <summary>
        /// Xuất danh sách tất cả nhân viên ra excel
        /// </summary>
        /// <returns>file excel chứa danh sách nhân vie</returns>
        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            MemoryStream memoryStream = await _employeeService.ExportExcelAsync();

            var contentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = ResourceVN.Export_FileName,
            };
            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{ResourceVN.Export_FileName}");
        }

        [HttpGet("bntien")]
        public async Task<IActionResult> NameAuthor()
        {
            return Ok("Bùi Ngọc Tiến");
        }
        #endregion
    }
}
