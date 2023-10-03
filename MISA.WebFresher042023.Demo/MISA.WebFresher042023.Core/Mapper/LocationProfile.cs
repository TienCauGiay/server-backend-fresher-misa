using AutoMapper;
using MISA.WebFresher042023.Core.DTO.Location;
using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Mapper
{
    /// <summary>
    /// class chuyển đổi dữ liệu vị trí địa lí sang các đối tượng phù hợp
    /// </summary>
    /// Created By: BNTIEN (16/08/2023)
    public class LocationProfile : Profile
    {
        public LocationProfile() 
        {
            CreateMap<Location, LocationDto>();
            CreateMap<LocationCountryDto, Location>();
            CreateMap<LocationCityDto, Location>();
            CreateMap<LocationDistrictDto, Location>();
            CreateMap<LocationVillageDto, Location>();
            CreateMap<LocationCreateDto, Location>();
            CreateMap<LocationUpdateDto, Location>();
        }
    }
}
