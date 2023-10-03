using AutoMapper;
using MISA.WebFresher042023.Core.DTO.Groups;
using MISA.WebFresher042023.Core.DTO.Providers;
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
    public class GroupService : BaseService<Group, GroupDto, GroupCreateDto, GroupUpdateDto>, IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUnitOfWork _unitOfWork;
        public GroupService(IUnitOfWork unitOfWork, IGroupRepository groupRepository, IMapper mapper) : base(unitOfWork, groupRepository, mapper)
        {
            _groupRepository = groupRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Tìm kiếm phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        public async Task<FilterGroupDto?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch)
        {
            var filterGroup = await _groupRepository.GetFilterAsync(pageSize, pageNumber, textSearch);
            if (filterGroup?.Data != null)
            {
                var filterGroupDto = _mapper.Map<FilterGroupDto>(filterGroup);
                return filterGroupDto;
            }
            return null;
        }
    }
}
