using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.WebFresher042023.Core.DTO.Accounts;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Interfaces.Infrastructures;
using MISA.WebFresher042023.Core.Interfaces.UnitOfWork;
using MISA.WebFresher042023.Core.Resources;
using MySqlConnector;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher042023.Core.Enums;
using System.Transactions;
using MISA.WebFresher042023.Infrastructure.Common.Procedure;

namespace MISA.WebFresher042023.Infrastructure.Repository
{
    /// <summary>
    /// class triển khai các phương thức của thực thể account truy vấn cơ sở dữ liệu
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// Created By: BNTIEN (19/07/2023)
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        /// <summary>
        /// Hàm tạo
        /// </summary>
        /// <param name="configuration"></param>
        public AccountRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region Method riêng (Account)

        /// <summary>
        /// Tìm kiếm và phân trang trên giao diện
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns>Danh sách tài khoản theo tìm kiếm, phân trang</returns>
        /// Created By: BNTIEN (19/07/2023)
        public async Task<FilterAccount?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch)
        {
            textSearch = textSearch ?? string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@PageNumber", pageNumber);
            parameters.Add("@TextSearch", textSearch);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _unitOfWork.Connection.QueryAsync<Account>(ProcConstantAccount.GET_FILTER, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
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

            return new FilterAccount
            {
                TotalPage = (int)Math.Ceiling((decimal)totalRecord / pageSize),
                TotalRecord = totalRecord,
                CurrentPage = pageNumber,
                CurrentPageRecords = currentPageRecords,
                Data = result.ToList()
            };
        }

        /// <summary>
        /// Đếm xem 1 node cha có bao nhiêu con
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns>Số lượng code của node cha đó</returns>
        /// Created By: BNTIEN (21/07/2023)
        public async Task<(Account?, int)> GetCountChildren(string parentId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ParentId", parentId);
            parameters.Add("@CountChildren", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var res = await _unitOfWork.Connection.QueryAsync<Account>(ProcConstantAccount.GET_COUNT_CHILDREN, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            if(res != null && res.Count() > 0)
            {
                return (res.ElementAt(0), parameters.Get<int>("@CountChildren"));
            }
            return (null, 0);
        }

        /// <summary>
        /// Lấy danh sách các con của tài khoản có số tài khoản là tham số truyền vào
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns>Danh sách tài khoản con</returns>
        /// Created By: BNTIEN (21/07/2023)
        public async Task<List<Account>?> GetAllChildrenAsync(string parentId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ParentId", parentId);

            var res = await _unitOfWork.Connection.QueryAsync<Account>(ProcConstantAccount.GET_ALL_CHILDREN, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);

            return (List<Account>?)res;
        }

        /// <summary>
        /// Lấy danh sách tài khoản tổng hợp
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (26/07/2023)
        public async Task<FilterAccount?> GetBySearchFilterAsync(int pageSize, int pageNumber, string? textSearch)
        {
            textSearch = textSearch ?? string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@PageNumber", pageNumber);
            parameters.Add("@TextSearch", textSearch);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _unitOfWork.Connection.QueryAsync<Account>(ProcConstantAccount.GET_BY_SEARCH_FILTER, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
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

            return new FilterAccount
            {
                TotalPage = (int)Math.Ceiling((decimal)totalRecord / pageSize),
                TotalRecord = totalRecord,
                CurrentPage = pageNumber,
                CurrentPageRecords = currentPageRecords,
                Data = result.ToList()
            };
        }

        /// <summary>
        /// Lấy danh sách tài khoản theo đối tượng (đổi tên cột) thích hợp
        /// </summary>
        /// <typeparam name="TAccount"></typeparam>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <param name="storeProcedureName"></param>
        /// <returns>Danh sách tài khoản</returns
        /// Created By: BNTIEN (26/07/2023)
        public async Task<List<TAccount>> GetObjectAsync<TAccount>(int pageSize, int pageNumber, string? textSearch, string storeProcedureName)
        {
            textSearch = textSearch ?? string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@PageNumber", pageNumber);
            parameters.Add("@TextSearch", textSearch);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _unitOfWork.Connection.QueryAsync<TAccount>(storeProcedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return result.ToList();
        }

        /// <summary>
        /// Xử lí chức năng mở rộng trên giao diện
        /// </summary>
        /// <param name="account"></param>
        /// <returns>Danh sách tài khoản sau khi mở rộng</returns>
        /// Created By: BNTIEN (26/07/2023)
        public async Task<List<Account>?> GetExpandAsync(List<string> accountNumbers)
        {
            var query = new StringBuilder();
            var parameters = new DynamicParameters();
            var index = 0;
            foreach (var accountNumber in accountNumbers)
            {
                query.AppendLine($"SELECT a.AccountId,a.AccountNumber,a.AccountName,a.AccountNameEnglish,a.Nature,a.`Explain`,a.Grade,a.ParentId,a.IsParent,a.IsRoot,a.State,a.UserObject FROM account a WHERE a.AccountNumber LIKE CONCAT(@AccountNumber_{index}, '%')");
                parameters.Add($"@AccountNumber_{index}", accountNumber);
                index++;

                // Nếu chưa phải phần tử cuỗi
                if (accountNumber != accountNumbers.Last())
                {
                    query.AppendLine("UNION");
                }
            }
            var finalQuery = $"SELECT * FROM ({query.ToString()}) AS union_result ORDER BY AccountNumber";

            var result = await _unitOfWork.Connection.QueryAsync<Account>(finalQuery, parameters, transaction: _unitOfWork.Transaction);
            return result.ToList();
        }

        /// <summary>
        /// Lấy tất cả các tài khoản theo giá trị tìm kiếm
        /// </summary>
        /// <param name="textSearch"></param>
        /// <returns>Danh sách tài khoản</returns>
        /// Created By: BNTIEN (26/07/2023)
        public async Task<List<Account>?> GetBySearchAsync(string? textSearch)
        {
            textSearch = textSearch ?? string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("@TextSearch", textSearch);

            var result = await _unitOfWork.Connection.QueryAsync<Account>(ProcConstantAccount.GET_BY_SEARCH, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return result.ToList();
        }

        /// <summary>
        /// Lấy danh sách tài khoản theo danh sách số tài khoản
        /// </summary>
        /// <param name="accountNumbers"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (26/07/2023)
        public async Task<List<Account>?> GetByListCodeAsync(List<string> accountNumbers)
        {
            var parameters = new DynamicParameters();
            parameters.Add("accountNumbers", accountNumbers);

            string query = $"SELECT a.AccountId, a.AccountNumber, a.AccountName, a.AccountNameEnglish, a.Nature, a.`Explain`, a.Grade, a.ParentId, a.IsParent, a.IsRoot, a.State, a.UserObject FROM account a " +
                $"WHERE a.AccountNumber IN @accountNumbers ORDER BY a.AccountNumber";
            var res = await _unitOfWork.Connection.QueryAsync<Account>(query, parameters, _unitOfWork.Transaction);

            return (List<Account>?)res;
        }

        /// <summary>
        /// Cập nhật trạng thái cho các tài khoản
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="state"></param>
        /// <param name="isUpdateChildren"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi cập nhật</returns>
        /// Created By: BNTIEN (26/07/2023)
        public async Task<int> UpdateStateAsync(string accountNumber, int state, int isUpdateChildren)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@accountNumber", accountNumber);
            parameters.Add("@state", state);
            parameters.Add("@isUpdateChildren", isUpdateChildren);

            var rowsAffected = await _unitOfWork.Connection.ExecuteAsync(ProcConstantAccount.PUT_STATE, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return rowsAffected;
        }

        /// <summary>
        /// Xuất excel
        /// </summary>
        /// <param name="accounts"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (26/07/2023)
        public async Task<MemoryStream> ExportExcelAsync(List<Account>? accounts)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Or LicenseContext.Commercial
            using (var package = new ExcelPackage())
            {
                // Tên của Worksheet
                var worksheet = package.Workbook.Worksheets.Add(AccountVN.Export_Title_Account);

                // Định dạng Tiêu đề trang
                worksheet.Cells["A1:G1"].Merge = true;
                worksheet.Cells["A1:G1"].Value = AccountVN.Export_Title_Account;
                worksheet.Cells["A1:G1"].Style.Font.Bold = true;
                worksheet.Cells["A1:G1"].Style.Font.Size = 20;
                worksheet.Cells["A1:G1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Định dạng tiêu đề các cột
                worksheet.Cells["A3:G3"].Style.Font.Bold = true;
                worksheet.Cells["A3"].Value = ResourceVN.Export_Title_ColumnSTT;
                worksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["B3"].Value = AccountVN.Export_Title_AccountNumber;
                worksheet.Cells["C3"].Value = AccountVN.Export_Title_AccountName;
                worksheet.Cells["D3"].Value = AccountVN.Export_Title_Nature;
                worksheet.Cells["E3"].Value = AccountVN.Export_Title_AccountNameEnglish;
                worksheet.Cells["F3"].Value = AccountVN.Export_Title_Explain;
                worksheet.Cells["G3"].Value = AccountVN.Export_Title_State;

                if (accounts != null && accounts.Count > 0)
                {
                    // Ghi dữ liệu
                    for (int i = 0; i < accounts.Count; i++)
                    {
                        var account = accounts[i];
                        int rowIndex = i + 4; // Vị trí dòng bắt đầu từ dòng thứ 4

                        worksheet.Cells["A" + rowIndex].Value = i + 1; // Số thứ tự
                                                                       // Tính toán thụt lề dựa trên cấp "Grade"
                        int indentLevel = (int)account.Grade; // Cấp càng cao thụt vào càng nhiều

                        // Tạo chuỗi khoảng trắng để tạo thụt lề
                        string indentSpaces = new string(' ', indentLevel * 3);

                        // Áp dụng thụt lề cho cột số tài khoản
                        string accountNumberCell = "B" + rowIndex;
                        worksheet.Cells[accountNumberCell].Value = indentSpaces + account.AccountNumber;

                        worksheet.Cells["C" + rowIndex].Value = account.AccountName;
                        worksheet.Cells["D" + rowIndex].Value = account.Nature;
                        worksheet.Cells["E" + rowIndex].Value = account.AccountNameEnglish;
                        worksheet.Cells["F" + rowIndex].Value = account.Explain;
                        worksheet.Cells["G" + rowIndex].Value = account.State == BoolNumber.True ? AccountVN.Text_State_Using : AccountVN.Text_State_StopUsing;

                        // Định dạng số thứ tự và căn giữa các ô dữ liệu
                        worksheet.Cells["A" + rowIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["A" + rowIndex].Style.Numberformat.Format = "0";

                        // Kiểm tra nếu account.IsParent là true thì đặt định dạng đậm cho dòng hiện tại
                        if (account.IsParent == BoolNumber.True)
                        {
                            worksheet.Cells["A" + rowIndex + ":G" + rowIndex].Style.Font.Bold = true;
                        }
                    }
                    var dataRange = worksheet.Cells["A3:G" + (accounts.Count + 3)];
                    // Set border cho các cột và hàng
                    dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }

                // Đặt chiều rộng cột tự động hiển thị đủ nội dung
                worksheet.Cells["A:G"].AutoFitColumns();

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
