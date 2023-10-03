using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Infrastructure.Common.Procedure
{
    public static class ProcConstantGroupProvider
    {
        /// <summary>
        /// Tên thủ tục cập nhật thông tin 1 nhóm nhà cung cấp
        /// </summary>
        public const string PUT_GROUP_PROVIDER = "Proc_GroupProvider_Update";

        /// <summary>
        /// Tên thủ tục lấy danh sách nhóm nhà cung cấp theo id nhà cung cấp
        /// </summary>
        public const string GET_BY_PROVIDER_ID = "Proc_GroupProvider_GetByProviderId";
    }
}
