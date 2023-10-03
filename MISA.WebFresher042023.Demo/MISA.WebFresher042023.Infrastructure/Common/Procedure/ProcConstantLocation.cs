using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Infrastructure.Common.Procedure
{
    public static class ProcConstantLocation
    {
        /// <summary>
        /// Tên thủ tục lấy danh sách vị trí địa lí theo quốc gia
        /// </summary>
        public const string GET_LOCATION_COUNTRY = "Proc_Location_GetCountry";

        /// <summary>
        /// Tên thủ tục lấy danh sách vị trí địa lí theo tỉnh/thành phố
        /// </summary>
        public const string GET_LOCATION_CITY = "Proc_Location_GetCity";

        /// <summary>
        /// Tên thủ tục lấy danh sách vị trí địa lí theo quận/huyện
        /// </summary>
        public const string GET_LOCATION_DISTRICT = "Proc_Location_GetDistrict";

        /// <summary>
        /// Tên thủ tục lấy danh sách vị trí địa lí theo xã/phường
        /// </summary>
        public const string GET_LOCATION_VILLAGE = "Proc_Location_GetVillage";
    }
}
