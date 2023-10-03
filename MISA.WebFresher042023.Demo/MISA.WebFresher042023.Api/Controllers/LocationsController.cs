using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Core.DTO.Location;
using MISA.WebFresher042023.Core.Interfaces.Services;

namespace MISA.WebFresher042023.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LocationsController : BaseController<LocationDto, LocationCreateDto, LocationUpdateDto>
    {
        private readonly ILocationService _locationService;
        public LocationsController(ILocationService locationService) : base(locationService)
        {
            _locationService = locationService;
        }

        /// <summary>
        /// Lấy danh sách vị trí địa lí theo cấp
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilter(int grade, string? parentCode)
        {
            if(grade == 1)
            {
                var res = await _locationService.GetLocationCountryAsync(grade, parentCode);
                return Ok(res);
            }
            if(grade == 2)
            {
                var res = await _locationService.GetLocationCityAsync(grade, parentCode);
                return Ok(res);
            }
            if (grade == 3)
            {
                var res = await _locationService.GetLocationDistrictAsync(grade, parentCode);
                return Ok(res);
            }
            if (grade == 4)
            {
                var res = await _locationService.GetLocationVillageAsync(grade, parentCode);
                return Ok(res);
            }
            return Ok();
        }
    }
}
