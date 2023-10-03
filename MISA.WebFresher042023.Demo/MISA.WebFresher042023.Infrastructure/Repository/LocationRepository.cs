using AutoMapper;
using Dapper;
using MISA.WebFresher042023.Core.DTO.Location;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Interfaces.Infrastructures;
using MISA.WebFresher042023.Core.Interfaces.UnitOfWork;
using MISA.WebFresher042023.Infrastructure.Common.Procedure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Infrastructure.Repository
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        private readonly IMapper _mapper;

        private List<Location>? locations;
        public LocationRepository(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Lấy danh sách vị trí địa lí theo quốc gia
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        public async Task<List<LocationCountryDto>?> GetLocationCountryAsync(int grade, string? parentCode)
        {
            parentCode = parentCode ?? string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("@parentCode", parentCode);


            var res = (List<LocationCountryDto>?)await _unitOfWork.Connection.QueryAsync<LocationCountryDto>(ProcConstantLocation.GET_LOCATION_COUNTRY, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return res;
        }

        /// <summary>
        /// Lấy danh sách vị trí địa lí theo tỉnh/thành phố
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        public async Task<List<LocationCityDto>?> GetLocationCityAsync(int grade, string? parentCode)
        {
            parentCode = parentCode ?? string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("@parentCode", parentCode);


            var res = (List<LocationCityDto>?)await _unitOfWork.Connection.QueryAsync<LocationCityDto>(ProcConstantLocation.GET_LOCATION_CITY, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return res;
        }

        /// <summary>
        /// Lấy danh sách vị trí địa lí theo quận/huyện
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        public async Task<List<LocationDistrictDto>?> GetLocationDistrictAsync(int grade, string? parentCode)
        {
            parentCode = parentCode ?? string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("@parentCode", parentCode);


            var res = (List<LocationDistrictDto>?)await _unitOfWork.Connection.QueryAsync<LocationDistrictDto>(ProcConstantLocation.GET_LOCATION_DISTRICT, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return res;
        }

        /// <summary>
        /// Lấy danh sách vị trí địa lí theo xã/phường
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        public async Task<List<LocationVillageDto>?> GetLocationVillageAsync(int grade, string? parentCode)
        {
            parentCode = parentCode ?? string.Empty;
            var parameters = new DynamicParameters();
            parameters.Add("@parentCode", parentCode);


            var res = (List<LocationVillageDto>?)await _unitOfWork.Connection.QueryAsync<LocationVillageDto>(ProcConstantLocation.GET_LOCATION_VILLAGE, parameters, commandType: CommandType.StoredProcedure, transaction: _unitOfWork.Transaction);
            return res;
        }
    }
}
