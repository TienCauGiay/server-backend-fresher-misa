using AutoMapper;
using MISA.WebFresher042023.Core.DTO.Groups;
using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Mapper
{
    /// <summary>
    /// class chuyển đổi dữ liệu nhóm sang các đối tượng phù hợp
    /// </summary>
    /// Created By: BNTIEN (16/08/2023)
    public class GroupProfile : Profile
    {
        public GroupProfile() 
        {
            CreateMap<Group, GroupDto>();
            CreateMap<GroupCreateDto, Group>();
            CreateMap<GroupUpdateDto, Group>();
            CreateMap<FilterGroup, FilterGroupDto>();
        }
    }
}
