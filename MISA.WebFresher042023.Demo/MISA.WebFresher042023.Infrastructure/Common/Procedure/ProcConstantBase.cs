using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Infrastructure.Common.Procedure
{
    public static class ProcConstantBase
    {
        /// <summary>
        /// Tên thủ tục lấy danh sách đối tượng
        /// </summary>
        public const string GET_ALL = "Proc_{0}_GetAll";

        /// <summary>
        /// Tên thủ tục tìm kiếm đối tượng theo code
        /// </summary>
        public const string GET_BY_CODE = "Proc_{0}_GetByCode";

        /// <summary>
        /// Tên thủ tục tìm kiếm đối tượng theo id
        /// </summary>
        public const string GET_BY_ID = "Proc_{0}_GetById";

        /// <summary>
        /// Tên thủ tục thêm mới 1 đối tượng
        /// </summary>
        public const string POST_OBJECT = "Proc_{0}_Insert";

        /// <summary>
        /// Tên thủ tục sửa 1 đối tượng
        /// </summary>
        public const string PUT_OBJECT = "Proc_{0}_Update";

        /// <summary>
        /// Tên thủ tục xóa 1 đối tượng theo id (khóa chính)
        /// </summary>
        public const string DELETE_OBJECT = "Proc_{0}_Delete";
    }
}
