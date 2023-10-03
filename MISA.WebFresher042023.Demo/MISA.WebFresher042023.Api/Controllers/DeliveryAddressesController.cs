using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Core.DTO.DeliveryAddresses;
using MISA.WebFresher042023.Core.Interfaces.Services;
using MISA.WebFresher042023.Core.Services;

namespace MISA.WebFresher042023.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DeliveryAddressesController : BaseController<DeliveryAddressDto, DeliveryAddressCreateDto, DeliveryAddressUpdateDto>
    {
        private readonly IDeliveryAddressService _deliveryAddressService;
        public DeliveryAddressesController(IDeliveryAddressService deliveryAddressService) : base(deliveryAddressService)
        {
            _deliveryAddressService = deliveryAddressService;
        }

        /// <summary>
        /// Lấy danh sách địa chỉ giao hàng theo id nhà cung cấp
        /// </summary>
        /// <param name="providerId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        [HttpGet("id/{providerId}")]
        public async Task<IActionResult> GetByProviderId(Guid providerId)
        {
            var res = await _deliveryAddressService.GetByProviderIdAsync(providerId);
            return Ok(res);
        }
    }
}
