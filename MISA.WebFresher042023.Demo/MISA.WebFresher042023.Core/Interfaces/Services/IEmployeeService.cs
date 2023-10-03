﻿using MISA.WebFresher042023.Core.DTO.Employees;
using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Services
{
    /// <summary>
    /// Interface employee service
    /// </summary>
    /// Created By: BNTIEN (17/06/2023)
    public interface IEmployeeService : IBaseService<EmployeeDto, EmployeeCreateDto, EmployeeUpdateDto>
    {
        #region Method riêng (Employee)
        /// <summary>
        /// Lấy mã nhân viên lớn nhất trong hệ thống
        /// </summary>
        /// <returns>Mã nhân viên</returns>
        /// Created By: BNTIEN (17/06/2023)
        Task<string?> GetByCodeMaxAsync();

        /// <summary>
        /// Tìm kiếm và phân trang trên giao diện
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns>Danh sách nhân viên theo tìm kiếm, phân trang</returns>
        /// Created By: BNTIEN (17/06/2023)
        Task<FilterEmployeeDto?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch);

        /// <summary>
        /// Xuất danh sách toàn bộ nhân viên ra excel
        /// </summary>
        /// <returns>file excel chứa danh sách nhân viên</returns>
        /// Created By: BNTIEN (03/07/2023)
        Task<MemoryStream> ExportExcelAsync();
        #endregion
    }
}
