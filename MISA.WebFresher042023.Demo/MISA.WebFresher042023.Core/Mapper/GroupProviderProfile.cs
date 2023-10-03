using AutoMapper;
using MISA.WebFresher042023.Core.DTO.GroupProviders;
using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Mapper
{
    /// <summary>
    /// class chuyển đổi dữ liệu nhóm nhà cung cấp sang các đối tượng phù hợp
    /// </summary>
    /// Created By: BNTIEN (27/07/2023)
    public class GroupProviderProfile : Profile
    {
        public GroupProviderProfile() 
        {
            CreateMap<GroupProvider, GroupProviderDto>();
            CreateMap<GroupProviderCreateDto, GroupProvider>();
            CreateMap<GroupProviderUpdateDto, GroupProvider>();
            CreateMap<FilterGroupProvider, FilterGroupProviderDto>();
        }
    }
}
