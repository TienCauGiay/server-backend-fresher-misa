using Dapper;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Interfaces.Infrastructures;
using MISA.WebFresher042023.Core.Interfaces.UnitOfWork;
using MISA.WebFresher042023.Core.Resources;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using MISA.WebFresher042023.Infrastructure.Common.Procedure;

namespace MISA.WebFresher042023.Infrastructure.Repository
{
    public class ProviderRepository : BaseRepository<Provider>, IProviderRepository
    {
        private readonly IAccountProviderRepository _accountProviderRepository;
        private readonly IDeliveryAddressRepository _deliveryAddressRepository;
        private readonly IGroupProviderRepository _groupProviderRepository;
        public ProviderRepository(IUnitOfWork unitOfWork, IAccountProviderRepository accountProviderRepository,
            IDeliveryAddressRepository deliveryAddressRepository, IGroupProviderRepository groupProviderRepository) : base(unitOfWork)
        {
            _accountProviderRepository = accountProviderRepository;
            _deliveryAddressRepository = deliveryAddressRepository;
            _groupProviderRepository = groupProviderRepository;
        }

        /// <summary>
        /// Lấy mã nhà cung cấp lớn nhất trong hệ thống
        /// </summary>
        /// <returns></returns>
        /// Created By: BNTIEN (09/08/2023)
        public async Task<string?> GetByCodeMaxAsync()
        {
            var res = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<string>(ProcConstantProvider.GET_BY_CODE_MAX, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return !string.IsNullOrEmpty(res) ? res.ToString() : "";
        }

        /// <summary>
        /// Lấy danh sách nhà cung cấp xuất excel
        /// </summary>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (09/08/2023)
        public async Task<List<Provider>?> GetExportAsync(string? textSearch)
        {
            textSearch = textSearch ?? string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("@TextSearch", textSearch);
            var res = await _unitOfWork.Connection.QueryAsync<Provider>(ProcConstantProvider.GET_EXPORT, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return (List<Provider>?)res;
        }

        /// <summary>
        /// Tìm kiếm phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (09/08/2023)
        public async Task<FilterProvider?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch)
        {
            textSearch = textSearch ?? string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@PageNumber", pageNumber);
            parameters.Add("@TextSearch", textSearch);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var result = await _unitOfWork.Connection.QueryAsync<Provider>(ProcConstantProvider.GET_FILTER, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
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

            return new FilterProvider
            {
                TotalPage = (int)Math.Ceiling((decimal)totalRecord / pageSize),
                TotalRecord = totalRecord,
                CurrentPage = pageNumber,
                CurrentPageRecords = currentPageRecords,
                Data = result.ToList()
            };
        }

        /// <summary>
        /// Xuất excel
        /// </summary>
        /// <param name="providers"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (09/08/2023)
        public async Task<MemoryStream> ExportExcelAsync(List<Provider>? providers)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Or LicenseContext.Commercial
            using (var package = new ExcelPackage())
            {
                // Tên của Worksheet
                var worksheet = package.Workbook.Worksheets.Add(ProviderVN.Export_Title_Provider);

                // Định dạng Tiêu đề trang
                worksheet.Cells["A1:G1"].Merge = true;
                worksheet.Cells["A1:G1"].Value = ProviderVN.Export_Title_Provider;
                worksheet.Cells["A1:G1"].Style.Font.Bold = true;
                worksheet.Cells["A1:G1"].Style.Font.Size = 20;
                worksheet.Cells["A1:G1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Định dạng tiêu đề các cột
                worksheet.Cells["A3:G3"].Style.Font.Bold = true;
                worksheet.Cells["A3"].Value = ResourceVN.Export_Title_ColumnSTT;
                worksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells["B3"].Value = ProviderVN.Export_Title_ProviderCode;
                worksheet.Cells["C3"].Value = ProviderVN.Export_Title_ProviderName;
                worksheet.Cells["D3"].Value = ProviderVN.Export_Title_Address;
                worksheet.Cells["E3"].Value = ProviderVN.Export_Title_TaxCode;
                worksheet.Cells["F3"].Value = ProviderVN.Export_Title_Phone;
                worksheet.Cells["G3"].Value = ProviderVN.Export_Title_PurchasStaff;

                if(providers != null && providers.Count > 0)
                {
                    // Ghi dữ liệu
                    for (int i = 0; i < providers.Count; i++)
                    {
                        var provider = providers[i];
                        int rowIndex = i + 4; // Vị trí dòng bắt đầu từ dòng thứ 4

                        worksheet.Cells["A" + rowIndex].Value = i + 1; // Số thứ tự
                        worksheet.Cells["B" + rowIndex].Value = provider.ProviderCode;
                        worksheet.Cells["C" + rowIndex].Value = provider.ProviderName;
                        worksheet.Cells["D" + rowIndex].Value = provider.Address;
                        worksheet.Cells["E" + rowIndex].Value = provider.TaxCode;
                        worksheet.Cells["F" + rowIndex].Value = provider.PhoneNumber;
                        worksheet.Cells["G" + rowIndex].Value = provider.FullName;

                        // Định dạng số thứ tự và căn giữa các ô dữ liệu
                        worksheet.Cells["A" + rowIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells["A" + rowIndex].Style.Numberformat.Format = "0";
                    }
                    var dataRange = worksheet.Cells["A3:G" + (providers.Count + 3)];
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
    }
}
