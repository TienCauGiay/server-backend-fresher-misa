using MISA.WebFresher042023.Core.DTO.AccountProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Services
{
    public interface IAccountProviderService : IBaseService<AccountProviderDto, AccountProviderCreateDto, AccountProviderUpdateDto>
    {
        /// <summary>
        /// Lấy danh sách tài khoản ngân hàng theo id nhà cung cấp
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<List<AccountProviderDto>?> GetByProviderIdAsync(Guid providerId);
    }
}
