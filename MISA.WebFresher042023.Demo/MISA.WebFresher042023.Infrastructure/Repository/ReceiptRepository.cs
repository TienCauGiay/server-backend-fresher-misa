using Dapper;
using MISA.WebFresher042023.Core.DTO.Receipts;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Enums;
using MISA.WebFresher042023.Core.Interfaces.Infrastructures;
using MISA.WebFresher042023.Core.Interfaces.UnitOfWork;
using MISA.WebFresher042023.Core.Resources;
using MISA.WebFresher042023.Infrastructure.Common.Procedure;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Infrastructure.Repository
{
    public class ReceiptRepository : BaseRepository<Receipt>, IReceiptRepository
    {
        public ReceiptRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Lấy số phiếu chi lớn nhất trong hệ thống
        /// </summary>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        public async Task<string?> GetByCodeMaxAsync()
        {
            var res = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<string>(ProcConstantReceipt.GET_BY_CODE_MAX, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return !string.IsNullOrEmpty(res) ? res.ToString() : "";
        }

        /// <summary>
        /// Tìm kiếm phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <param name="keyFilter"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        public async Task<FilterReceipt?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch, bool? keyFilter)
        {
            textSearch = textSearch ?? string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@PageNumber", pageNumber);
            parameters.Add("@TextSearch", textSearch);
            parameters.Add("@KeyFilter", keyFilter);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _unitOfWork.Connection.QueryAsync<Receipt>(ProcConstantReceipt.GET_FILTER, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            var totalRecord = parameters.Get<int>("@TotalRecord");


            var currentPageRecords = 0;
            if (pageNumber < Math.Ceiling((decimal)totalRecord / pageSize))
            {
                currentPageRecords = pageSize;
            }
            else if (pageNumber == Math.Ceiling((decimal)totalRecord / pageSize))
            {
                currentPageRecords = totalRecord - (pageNumber - 1) * pageSize;
            }

            return new FilterReceipt
            {
                TotalPage = (int)Math.Ceiling((decimal)totalRecord / pageSize),
                TotalRecord = totalRecord,
                CurrentPage = pageNumber,
                CurrentPageRecords = currentPageRecords,
                Data = result.ToList()
            };
        }

        /// <summary> 
        /// Cập nhật trạng thái ghi sổ/bỏ ghi 1 bản ghi
        /// </summary>
        /// <param name="receipt"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        public async Task<int> UpdateNoteAsync(Receipt receipt)
        {
            var state = !receipt.IsNoted;
            var parameters = new DynamicParameters();
            parameters.Add("@Id", receipt.ReceiptId);
            parameters.Add("@State", state);

            var rowsAffected = await _unitOfWork.Connection.ExecuteAsync(ProcConstantReceipt.PUT_NOTE, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return rowsAffected;
        }

        /// <summary>
        /// Xuất file excel
        /// </summary>
        /// <param name="receipts"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        public async Task<MemoryStream> ExportExcelAsync(List<Receipt>? receipts)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Or LicenseContext.Commercial
            using (var package = new ExcelPackage())
            {
                // Tên của Worksheet
                var worksheet = package.Workbook.Worksheets.Add(ReceiptVN.Export_Title_Receipt);
                var currencyFormat = "#,##0.00_);(#,##0.00)";

                // Định dạng Tiêu đề trang
                worksheet.Cells["A1:J1"].Merge = true;
                worksheet.Cells["A1:J1"].Value = ReceiptVN.Export_Title_Receipt;
                worksheet.Cells["A1:J1"].Style.Font.Bold = true;
                worksheet.Cells["A1:J1"].Style.Font.Size = 20;
                worksheet.Cells["A1:J1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Định dạng tiêu đề các cột
                worksheet.Cells["A3:J3"].Style.Font.Bold = true;
                worksheet.Cells["A3:J3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; ;
                worksheet.Cells["A3"].Value = ResourceVN.Export_Title_ColumnSTT;
                worksheet.Cells["B3"].Value = ReceiptVN.Export_Title_AccountingDate;
                worksheet.Cells["C3"].Value = ReceiptVN.Export_Title_ReceiptDate;
                worksheet.Cells["D3"].Value = ReceiptVN.Export_Title_ReceiptNumber;
                worksheet.Cells["E3"].Value = ReceiptVN.Export_Title_TotalMoney;
                worksheet.Cells["F3"].Value = ReceiptVN.Export_Title_Reason;
                worksheet.Cells["G3"].Value = ReceiptVN.Export_Title_Obj;
                worksheet.Cells["H3"].Value = ReceiptVN.Export_Title_ObjCode;
                worksheet.Cells["I3"].Value = ReceiptVN.Export_Title_Address;
                worksheet.Cells["J3"].Value = ReceiptVN.Export_Title_TypeReceipt;

                decimal totalMoney = 0;

                // Ghi dữ liệu
                if(receipts != null && receipts.Count > 0) 
                {
                    for (int i = 0; i < receipts.Count; i++)
                    {
                        var receipt = receipts[i];
                        totalMoney += receipt.TotalMoney;
                        int rowIndex = i + 4; // Vị trí dòng bắt đầu từ dòng thứ 4

                        worksheet.Cells["A" + rowIndex].Value = i + 1; // Số thứ tự
                        worksheet.Cells["B" + rowIndex].Value = receipt.AccountingDate;
                        worksheet.Cells["C" + rowIndex].Value = receipt.ReceiptDate;
                        worksheet.Cells["D" + rowIndex].Value = receipt.ReceiptNumber;

                        var moneyCell = worksheet.Cells["E" + rowIndex];
                        moneyCell.Value = receipt.TotalMoney;
                        moneyCell.Style.Numberformat.Format = currencyFormat;

                        worksheet.Cells["F" + rowIndex].Value = receipt.Reason;
                        worksheet.Cells["G" + rowIndex].Value = receipt.ProviderName;
                        worksheet.Cells["H" + rowIndex].Value = receipt.ProviderCode;
                        worksheet.Cells["I" + rowIndex].Value = receipt.Address;
                        worksheet.Cells["J" + rowIndex].Value = receipt.ReceiptType == true ? ReceiptVN.Text_Receipt : ReceiptVN.Text_Payment;

                        // Định dạng số thứ tự và căn giữa các ô dữ liệu
                        worksheet.Cells["A" + rowIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["A" + rowIndex].Style.Numberformat.Format = "0";
                        worksheet.Cells["B" + rowIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["B" + rowIndex].Style.Numberformat.Format = "dd/MM/yyyy";
                        worksheet.Cells["C" + rowIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["C" + rowIndex].Style.Numberformat.Format = "dd/MM/yyyy";
                        worksheet.Cells["E" + rowIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        if (receipt.TotalMoney < 0)
                        {
                            moneyCell.Style.Font.Color.SetColor(Color.Red);
                        }

                        // Định dạng các cột chiều rộng tự động xuống dòng nếu quá rộng
                        worksheet.Cells["A" + rowIndex + ":J" + rowIndex].Style.WrapText = true;
                    }
                    // Dòng tổng tiền
                    int totalRowIndex = receipts.Count + 4;
                    worksheet.Cells["B" + totalRowIndex].Value = ReceiptVN.Text_Total;
                    worksheet.Cells["E" + totalRowIndex].Value = totalMoney;
                    worksheet.Cells["B" + totalRowIndex].Style.Font.Bold = true;
                    worksheet.Cells["E" + totalRowIndex].Style.Font.Bold = true;
                    worksheet.Cells["E" + totalRowIndex].Style.Numberformat.Format = currencyFormat;
                    if (totalMoney < 0)
                    {
                        worksheet.Cells["E" + totalRowIndex].Style.Font.Color.SetColor(Color.Red);
                    }
                    // Set border cho các cột và hàng
                    var dataRange = worksheet.Cells["A3:J" + (totalRowIndex - 1)];
                    dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }

                // Đặt chiều rộng cột tự động hiển thị đủ nội dung
                worksheet.Cells["A:J"].AutoFitColumns();

                // Setup lưu file
                using (var memoryStream = new MemoryStream())
                {
                    await package.SaveAsAsync(memoryStream);

                    memoryStream.Position = 0;
                    return memoryStream;
                }
            }
        }

        /// <summary>
        /// Lấy danh sách xuất excel
        /// </summary>
        /// <param name="textSearch"></param>
        /// <param name="keyFilter"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        public async Task<List<Receipt>?> GetExportAsync(string? textSearch, bool? keyFilter)
        {
            textSearch = textSearch ?? string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("@TextSearch", textSearch);
            parameters.Add("@KeyFilter", keyFilter);

            var result = await _unitOfWork.Connection.QueryAsync<Receipt>(ProcConstantReceipt.GET_EXPORT, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return (List<Receipt>?)result;
        }

        /// <summary>
        /// Lấy danh sách phiếu chi đang ở trạng thái ghi sổ/bỏ ghi theo list receipt id (để thực hiên cập nhật trạng thái ghi sổ/bỏ ghi nhiều phiếu chi)
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        public async Task<List<Receipt>?> GetSatisfiedAsync(List<Guid> ids, ReceiptNote note)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ids", ids);
            parameters.Add("note", note);
            string query = $"SELECT r.ReceiptId FROM receipt r WHERE r.ReceiptId IN @ids AND r.IsNoted = @note";
            var res = await _unitOfWork.Connection.QueryAsync<Receipt>(query, parameters, _unitOfWork.Transaction);

            return (List<Receipt>?)res;
        }

        /// <summary>
        /// Cập nhật trạng thái ghi sổ/bỏ ghi nhiều bản ghi
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="typeUpdate"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        public async Task<int> UpdateNoteMultipleAsync(List<Guid> ids, bool typeUpdate)
        {
            var parameters = new DynamicParameters();
            parameters.Add("ids", ids);
            string query = "";
            if (typeUpdate == true) // Nếu từ chưa ghi sổ => ghi sổ
            {
                query = $"UPDATE receipt r SET r.IsNoted = 1 WHERE r.ReceiptId IN @ids";
            } else if(typeUpdate == false)  // Nếu từ ghi sổ => bỏ ghi 
            {
                query = $"UPDATE receipt r SET r.IsNoted = 0 WHERE r.ReceiptId IN @ids";
            }
            var res = await _unitOfWork.Connection.ExecuteAsync(query, parameters, _unitOfWork.Transaction);
            return res;
        }
    }
}
