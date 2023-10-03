using AutoMapper;
using MISA.WebFresher042023.Core.DTO.AccountProviders;
using MISA.WebFresher042023.Core.DTO.DeliveryAddresses;
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
    public class DeliveryAddressService : BaseService<DeliveryAddress, DeliveryAddressDto, DeliveryAddressCreateDto, DeliveryAddressUpdateDto>, IDeliveryAddressService
    {
        private readonly IDeliveryAddressRepository _deliveryAddressRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeliveryAddressService(IUnitOfWork unitOfWork, IDeliveryAddressRepository deliveryAddressRepository, IMapper mapper) : base(unitOfWork, deliveryAddressRepository, mapper)
        {
            _deliveryAddressRepository = deliveryAddressRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Lấy danh sách địa chỉ giao hàng theo id nhà cung cấp
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        public async Task<List<DeliveryAddressDto>?> GetByProviderIdAsync(Guid providerId)
        {
            var deliveryAddresses = await _deliveryAddressRepository.GetByProviderIdAsync(providerId);
            if (deliveryAddresses != null)
            {
                var res = _mapper.Map<List<DeliveryAddressDto>>(deliveryAddresses);
                return res;
            }
            return null;
        }
    }
}
