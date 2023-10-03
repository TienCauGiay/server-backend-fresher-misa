using AutoMapper;
using MISA.WebFresher042023.Core.DTO.Location;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Interfaces.Infrastructures;
using MISA.WebFresher042023.Core.Interfaces.Services;
using MISA.WebFresher042023.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Services
{
    public class LocationService : BaseService<Location, LocationDto, LocationCreateDto, LocationUpdateDto>, ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LocationService(IUnitOfWork unitOfWork, ILocationRepository locationRepository, IMapper mapper) : base(unitOfWork, locationRepository, mapper)
        {
            _locationRepository = locationRepository;
            _unitOfWork = unitOfWork;
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
            var locations = await _locationRepository.GetLocationCountryAsync(grade, parentCode);
            return locations;
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
            var locations = await _locationRepository.GetLocationCityAsync(grade, parentCode);
            return locations;
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
            var locations = await _locationRepository.GetLocationDistrictAsync(grade, parentCode);
            return locations;
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
            var locations = await _locationRepository.GetLocationVillageAsync(grade, parentCode);
            return locations;
        }
    }
}
