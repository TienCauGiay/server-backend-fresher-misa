using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Infrastructures
{
    public interface IGroupRepository : IBaseRepository<Group>
    {
        /// <summary>
        /// Tìm kiếm phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<FilterGroup?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch);

        /// <summary>
        /// Tìm kiếm group theo list group id (để validate khi thêm, sửa provider)
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<List<Group>?> GetByListId(List<Guid> ids);
    }
}
