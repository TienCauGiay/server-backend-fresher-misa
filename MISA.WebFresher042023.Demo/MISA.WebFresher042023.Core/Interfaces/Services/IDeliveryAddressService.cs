using MISA.WebFresher042023.Core.DTO.DeliveryAddresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Interfaces.Services
{
    public interface IDeliveryAddressService : IBaseService<DeliveryAddressDto, DeliveryAddressCreateDto, DeliveryAddressUpdateDto>
    {
        Task<List<DeliveryAddressDto>?> GetByProviderIdAsync(Guid providerId);
    }
}
