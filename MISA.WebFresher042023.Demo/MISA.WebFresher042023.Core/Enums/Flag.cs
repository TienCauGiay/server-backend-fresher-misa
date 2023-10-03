using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Enums
{
    /// <summary>
    /// Enum cờ theo dõi trạng thái update multiple 
    /// Quy ước: 0: Không thay đổi, 1: Thêm, 2: Sửa, 3: Xóa
    /// </summary>
    /// Created By: BNTIEN (09/08/2023)
    public enum E_Flag
    {
        NoChange = 0,
        Add = 1,
        Update = 2,
        Delete = 3,
    }
}
