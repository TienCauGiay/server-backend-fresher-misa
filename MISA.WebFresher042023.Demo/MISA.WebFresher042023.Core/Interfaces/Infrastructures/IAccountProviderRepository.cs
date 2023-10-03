using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Infrastructures
{
    public interface IAccountProviderRepository : IBaseRepository<AccountProvider>
    {
        /// <summary>
        /// Lấy danh sách tài khoản ngân hàng theo id nhà cung cấp
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<List<AccountProvider>?> GetByProviderIdAsync(Guid providerId);

        /// <summary>
        /// Xóa nhiều tài khoản ngân hàng theo nhiều id nhà cung cấp
        /// </summary>
        /// <param name="providerIds"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<int> DeleteMultipleByProviderId(List<Guid> providerIds);
    }
}
