using MISA.WebFresher042023.Core.DTO.Accountants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Services
{
    public interface IAccountantService : IBaseService<AccountantDto, AccountantCreateDto, AccountantUpdateDto>
    {
        /// <summary>
        /// Lấy danh sách hạch toán theo id phiếu chi
        /// </summary>
        /// <param name="receiptId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<List<AccountantDto>?> GetByReceiptIdAsync(Guid receiptId);
    }
}
