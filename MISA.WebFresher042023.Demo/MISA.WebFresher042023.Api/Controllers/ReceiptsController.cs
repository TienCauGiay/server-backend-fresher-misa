using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Core.DTO.Receipts;
using MISA.WebFresher042023.Core.Interfaces.Services;
using MISA.WebFresher042023.Core.Resources;
using MISA.WebFresher042023.Core.Services;
using System.Net.Http.Headers;

namespace MISA.WebFresher042023.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReceiptsController : BaseController<ReceiptDto, ReceiptCreateDto, ReceiptUpdateDto>
    {
        private readonly IReceiptService _receiptService;
        public ReceiptsController(IReceiptService receiptService) : base(receiptService)
        {
            _receiptService = receiptService;
        }

        /// <summary>
        /// Tìm kiếm phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <param name="keyFilter"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (15/08/2023)
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilter(int pageSize, int pageNumber, string? textSearch, bool? keyFilter)
        {
            var res = await _receiptService.GetFilterAsync(pageSize, pageNumber, textSearch, keyFilter);
            return Ok(res);
        }

        /// <summary>
        /// Lấy mã phiếu chi lớn nhất trong hệ thống
        /// </summary>
        /// <returns></returns>
        /// Created By: BNTIEN (15/08/2023)
        [HttpGet("maxcode")]
        public async Task<IActionResult> GetByCodeMax()
        {
            var res = await _receiptService.GetByCodeMaxAsync();
            return Ok(res);
        }

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="receiptCreateDto"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi thêm</returns>
        /// Created By: BNTIEN (17/06/2023)
        [HttpPost]
        public override async Task<IActionResult> Post(ReceiptCreateDto receiptCreateDto)
        {
            await _baseService.InsertAsync(receiptCreateDto);

            var receiptInserted = await _receiptService.GetByCodeAsync(receiptCreateDto.ReceiptNumber);

            return StatusCode(StatusCodes.Status201Created, receiptInserted?.ReceiptId);
        }

        /// <summary>
        /// Ghi sổ/Bỏ ghi 1 phiếu chi
        /// </summary>
        /// <param name="receiptUpdateDto"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (15/08/2023)
        [HttpPut("note")]
        public async Task<IActionResult> UpdateNote(ReceiptUpdateDto receiptUpdateDto)
        {
            var res = await _receiptService.UpdateNoteAsync(receiptUpdateDto);
            return Ok(res);
        }

        /// <summary>
        /// Ghi sổ/Bỏ ghi nhiều phiếu chi
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="typeUpdate"></param>
        /// <returns></returns>
        [HttpPut("notes")]
        public async Task<IActionResult> UpdateNoteMultiple(List<Guid> ids, bool typeUpdate)
        {
            var res = await _receiptService.UpdateNoteMultipleAsync(ids, typeUpdate);
            return Ok(res); 
        }

        /// <summary>
        /// Xuất excel
        /// </summary>
        /// <param name="textSearch"></param>
        /// <param name="keyFilter"></param>
        /// <returns></returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportExcel(string? textSearch, bool? keyFilter)
        {
            MemoryStream memoryStream = await _receiptService.ExportExcelAsync(textSearch, keyFilter);

            var contentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = ReceiptVN.Export_FileName,
            };
            Response.Headers.Add("Content-Disposition", contentDisposition.ToString());

            return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{ReceiptVN.Export_FileName}");
        }
    }
}
