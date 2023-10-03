using AutoMapper;
using MISA.WebFresher042023.Core.DTO.Providers;
using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Mapper
{
    /// <summary>
    /// class chuyển đổi dữ liệu nhà cung cấp sang các đối tượng phù hợp
    /// </summary>
    /// Created By: BNTIEN (16/08/2023)
    public class ProviderProfile : Profile
    {
        public ProviderProfile() 
        {
            CreateMap<Provider, ProviderDto>();
            CreateMap<ProviderCreateDto, Provider>();
            CreateMap<ProviderUpdateDto, Provider>();
            CreateMap<FilterProvider, FilterProviderDto>();
        }
    }
}
