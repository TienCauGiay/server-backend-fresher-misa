using MISA.WebFresher042023.Core.DTO.GroupProviders;
using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Services
{
    /// <summary>
    /// Interface GroupProvider service
    /// </summary>
    /// Created By: BNTIEN (27/07/2023)
    public interface IGroupProviderService : IBaseService<GroupProviderDto, GroupProviderCreateDto, GroupProviderUpdateDto>
    {
        /// <summary>
        /// Lấy danh sách nhóm nhà cung cấp theo id nhà cung cấp
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (27/07/2023)
        Task<List<GroupProviderDto>?> GetByProviderIdAsync(Guid id);
    }
}
