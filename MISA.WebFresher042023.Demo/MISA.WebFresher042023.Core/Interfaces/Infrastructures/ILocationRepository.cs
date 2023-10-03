using MISA.WebFresher042023.Core.DTO.Location;
using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Infrastructures
{
    public interface ILocationRepository : IBaseRepository<Location>
    {
        /// <summary>
        /// Lấy danh sách vị trí địa lí theo quốc gia
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<List<LocationCountryDto>?> GetLocationCountryAsync(int grade, string? parentCode);
        /// <summary>
        /// Lấy danh sách vị trí địa lí theo tỉnh/thành phố
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<List<LocationCityDto>?> GetLocationCityAsync(int grade, string? parentCode);
        /// <summary>
        /// Lấy danh sách vị trí địa lí theo quận/huyện
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<List<LocationDistrictDto>?> GetLocationDistrictAsync(int grade, string? parentCode);
        /// <summary>
        /// Lấy danh sách vị trí địa lí theo xã/phường
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        Task<List<LocationVillageDto>?> GetLocationVillageAsync(int grade, string? parentCode);
    }
}
