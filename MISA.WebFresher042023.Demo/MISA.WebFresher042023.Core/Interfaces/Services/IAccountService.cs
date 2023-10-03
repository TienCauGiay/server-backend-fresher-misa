using MISA.WebFresher042023.Core.DTO.Accounts;
using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Services
{
    /// <summary>
    /// Interface account service
    /// </summary>
    /// Created By: BNTIEN (19/07/2023)
    public interface IAccountService : IBaseService<AccountDto, AccountCreateDto, AccountUpdateDto>
    {
        /// <summary>
        /// Tìm kiếm và phân trang trên giao diện
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns>Danh sách tài khoản theo tìm kiếm, phân trang</returns>
        /// Created By: BNTIEN (19/07/2023)
        Task<FilterAccountDto?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch);

        /// <summary>
        /// Lấy danh sách các con của tài khoản có số tài khoản là tham số truyền vào
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns>Danh sách tài khoản con</returns>
        /// Created By: BNTIEN (21/07/2023)
        Task<List<AccountDto>?> GetAllChildrenAsync(string parentId);

        /// <summary>
        /// Cập nhật trạng thái cho các tài khoản
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="state"></param>
        /// <param name="isUpdateChildren"></param>
        /// <returns>Số hàng bị ảnh hưởng sau khi cập nhật</returns>
        /// Created By: BNTIEN (26/07/2023)
        Task<int> UpdateStateAsync(AccountUpdateDto account, int state, int isUpdateChildren);

        /// <summary>
        /// Tìm theo số tài khoản hoặc tên để load combobox
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns>Danh sách</returns>
        /// Created By: BNTIEN (26/07/2023)
        Task<FilterAccountDto?> GetBySearchFilterAsync(int pageSize, int pageNumber, string? textSearch);

        /// <summary>
        /// Lấy tất cả các tài khoản theo giá trị tìm kiếm
        /// </summary>
        /// <param name="textSearch"></param>
        /// <returns>Danh sách tài khoản</returns>
        /// Created By: BNTIEN (26/07/2023)
        Task<FilterAccountDto?> GetBySearchAsync(int pageSize, int pageNumber, string? textSearch);

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
        Task<List<TAccount>> GetObjectAsync<TAccount>(int pageSize, int pageNumber, string? textSearch, string storeProcedureName);

        /// <summary>
        /// Lấy danh sách khi chọn chức năng mở rộng trên giao diện
        /// </summary>
        /// <param name="account"></param>
        /// <returns>Danh sách tài khoản</returns>
        /// Created By: BNTIEN (26/07/2023)
        Task<List<Account>?> GetExpandAsync(int pageSize, int pageNumber, string? textSearch);

        /// <summary>
        /// Xuất excel
        /// </summary>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (26/07/2023)
        Task<MemoryStream> ExportExcelAsync(string? textSearch);
    }
}
