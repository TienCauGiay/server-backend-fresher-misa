using AutoMapper;
using MISA.WebFresher042023.Core.DTO.GroupProviders;
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
    /// <summary>
    /// Class triển khai các phương thức của entities group provider
    /// </summary>
    /// Created By: BNTIEN (27/07/2023)
    public class GroupProviderService : BaseService<GroupProvider, GroupProviderDto, GroupProviderCreateDto, GroupProviderUpdateDto>, IGroupProviderService
    {
        private readonly IGroupProviderRepository _groupProviderRepository;
        private readonly IUnitOfWork _unitOfWork;
        /// <summary>
        /// Hàm tạo
        /// </summary>
        /// <param name="baseRepository"></param>
        /// <param name="mapper"></param>
        public GroupProviderService(IUnitOfWork unitOfWork, IGroupProviderRepository groupProviderRepository, IMapper mapper) : base(unitOfWork, groupProviderRepository, mapper)
        {
            _groupProviderRepository = groupProviderRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Lấy danh sách nhóm nhà cung cấp theo id nhà cung cấp
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (27/07/2023)
        public async Task<List<GroupProviderDto>?> GetByProviderIdAsync(Guid id)
        {
            var groupProviders = await _groupProviderRepository.GetByProviderIdAsync(id);
            if (groupProviders != null)
            {
                var res = _mapper.Map<List<GroupProviderDto>>(groupProviders);
                return res;
            }
            return null;
        }
    }
}
