using AutoMapper;
using MISA.WebFresher042023.Core.DTO.DeliveryAddresses;
using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Mapper
{
    /// <summary>
    /// class chuyển đổi dữ liệu địa chỉ giao hàng sang các đối tượng phù hợp
    /// </summary>
    /// Created By: BNTIEN (16/08/2023)
    public class DeliveryAddressProfile : Profile
    {
        public DeliveryAddressProfile() 
        {
            CreateMap<DeliveryAddress, DeliveryAddressDto>();
            CreateMap<DeliveryAddressCreateDto, DeliveryAddress>();
            CreateMap<DeliveryAddressUpdateDto, DeliveryAddress>();
        }
    }
}
