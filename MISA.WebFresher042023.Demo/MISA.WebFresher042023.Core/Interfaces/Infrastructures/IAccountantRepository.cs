using MISA.WebFresher042023.Core.DTO.Accountants;
using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Infrastructures
{
    public interface IAccountantRepository : IBaseRepository<Accountant>
    {
        /// <summary>
        /// Lấy danh sách hạch toán theo id phiếu chi
        /// </summary>
        /// <param name="receiptId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<List<AccountantDto>?> GetByReceiptIdAsync(Guid receiptId);

        /// <summary>
        /// Xóa nhiều hạch toán theo id phiếu chi
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<int> DeleteMultipleByReceiptIdAsync(List<Guid> ids);

        /// <summary>
        /// Kiểm tra 1 số tài khoản có nằm trong bảng hạch toán hay không (để xóa tài khoản)
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<Accountant?> GetByAccountIdAsync(Guid accountId);
    }
}
