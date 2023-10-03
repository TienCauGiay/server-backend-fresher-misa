using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Core.DTO.Employees;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Interfaces.Infrastructures;
using MySqlConnector;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher042023.Core.Resources;
using MISA.WebFresher042023.Core.Interfaces.UnitOfWork;
using MISA.WebFresher042023.Infrastructure.Common.Procedure;

namespace MISA.WebFresher042023.Infrastructure.Repository
{
    /// <summary>
    /// class triển khai các phương thức của thực thể employee truy vấn cơ sở dữ liệu
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created By: BNTIEN (17/06/2023)
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        /// <summary>
        /// Hàm tạo
        /// </summary>
        /// <param name="configuration"></param>
        public EmployeeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region Method riêng (Employee)
        /// <summary>
        /// Lấy mã nhân viên lớn nhất trong hệ thống
        /// </summary>
        /// <returns>Mã nhân viên</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<string?> GetByCodeMaxAsync()
        {
            var res = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<string>(ProcConstantEmployee.GET_BY_CODE_MAX, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return !string.IsNullOrEmpty(res) ? res.ToString() : "";
        }

        /// <summary>
        /// Tìm kiếm và phân trang trên giao diện
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns>Danh sách nhân viên theo tìm kiếm, phân trang</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<FilterEmployee?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch)
        {
            textSearch = textSearch ?? string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@PageNumber", pageNumber);
            parameters.Add("@TextSearch", textSearch);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _unitOfWork.Connection.QueryAsync<Employee>(ProcConstantEmployee.GET_FILTER, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
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

            return new FilterEmployee
            {
                TotalPage = (int)Math.Ceiling((decimal)totalRecord / pageSize),
                TotalRecord = totalRecord,
                CurrentPage = pageNumber,
                CurrentPageRecords = currentPageRecords,
                Data = result.ToList()
            };
        }
        public override async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            var rowsAffected = await base.DeleteMultipleAsync(ids);
            return rowsAffected;
        }

        /// <summary>
        /// Xuất danh sách nhân viên ra excel
        /// </summary>
        /// <returns>file excel chứa danh sách nhân viên</returns>
        /// Created By: BNTIEN (03/07/2023)
        public async Task<MemoryStream> ExportExcelAsync(List<EmployeeExportDto> listEmployeeDto)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Or LicenseContext.Commercial
            using (var package = new ExcelPackage())
            {
                // Tên của Worksheet
                var worksheet = package.Workbook.Worksheets.Add(ResourceVN.Export_Title_Employee);

                // Định dạng Tiêu đề trang
                worksheet.Cells["A1:K1"].Merge = true;
                worksheet.Cells["A1:K1"].Value = ResourceVN.Export_Title_Employee;
                worksheet.Cells["A1:K1"].Style.Font.Bold = true;
                worksheet.Cells["A1:K1"].Style.Font.Size = 24;
                worksheet.Cells["A1:K1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Định dạng tiêu đề các cột
                worksheet.Cells["A3:K3"].Style.Font.Bold = true;
                worksheet.Cells["A3"].Value = ResourceVN.Export_Title_ColumnSTT;
                worksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["B3"].Value = ResourceVN.Export_Title_ColumnCode;
                worksheet.Cells["C3"].Value = ResourceVN.Export_Title_ColumnFulName;
                worksheet.Cells["D3"].Value = ResourceVN.Export_Title_ColumnGender;
                worksheet.Cells["E3"].Value = ResourceVN.Export_Title_ColumnDOB;
                worksheet.Cells["E3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["F3"].Value = ResourceVN.Export_Title_ColumnIdentityNumber;
                worksheet.Cells["G3"].Value = ResourceVN.Export_Title_ColumnPosition;
                worksheet.Cells["H3"].Value = ResourceVN.Export_Title_ColumnDepartment;
                worksheet.Cells["I3"].Value = ResourceVN.Export_Title_ColumnBankAccount;
                worksheet.Cells["J3"].Value = ResourceVN.Export_Title_ColumnBankName;
                worksheet.Cells["K3"].Value = ResourceVN.Export_Title_ColumnBankBranch;

                // Ghi dữ liệu
                for (int i = 0; i < listEmployeeDto.Count; i++)
                {
                    var employee = listEmployeeDto[i];
                    int rowIndex = i + 4; // Vị trí dòng bắt đầu từ dòng thứ 4

                    worksheet.Cells["A" + rowIndex].Value = i + 1; // Số thứ tự
                    worksheet.Cells["B" + rowIndex].Value = employee.EmployeeCode;
                    worksheet.Cells["C" + rowIndex].Value = employee.FullName;
                    worksheet.Cells["D" + rowIndex].Value = employee.GenderName;
                    worksheet.Cells["E" + rowIndex].Value = employee.DateOfBirth;
                    worksheet.Cells["F" + rowIndex].Value = employee.IdentityNumber;
                    worksheet.Cells["G" + rowIndex].Value = employee.PositionName;
                    worksheet.Cells["H" + rowIndex].Value = employee.DepartmentName;
                    worksheet.Cells["I" + rowIndex].Value = employee.BankAccount;
                    worksheet.Cells["J" + rowIndex].Value = employee.BankName;
                    worksheet.Cells["K" + rowIndex].Value = employee.BankBranch;

                    // Định dạng số thứ tự và căn giữa các ô dữ liệu
                    worksheet.Cells["A" + rowIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["A" + rowIndex].Style.Numberformat.Format = "0";
                    worksheet.Cells["E" + rowIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells["E" + rowIndex].Style.Numberformat.Format = "dd/MM/yyyy";
                }

                // Set border cho các cột và hàng
                var dataRange = worksheet.Cells["A3:K" + (listEmployeeDto.Count + 3)];
                dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                // Đặt chiều rộng cột tự động hiển thị đủ nội dung
                worksheet.Cells["A:K"].AutoFitColumns();

                // Setup lưu file
                using (var memoryStream = new MemoryStream())
                {
                    await package.SaveAsAsync(memoryStream);

                    memoryStream.Position = 0;
                    return memoryStream;
                }
            }
        }
        #endregion
    }
}
