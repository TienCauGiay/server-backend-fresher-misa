using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher042023.Core.DTO.Groups;
using MISA.WebFresher042023.Core.Interfaces.Services;
using MISA.WebFresher042023.Core.Services;

namespace MISA.WebFresher042023.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GroupsController : BaseController<GroupDto, GroupCreateDto, GroupUpdateDto>
    {
        private readonly IGroupService _groupService;
        public GroupsController(IGroupService groupService) : base(groupService)
        {
            _groupService = groupService;
        }

        /// <summary>
        /// Tìm kiếm phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilter(int pageSize, int pageNumber, string? textSearch)
        {
            var res = await _groupService.GetFilterAsync(pageSize, pageNumber, textSearch);
            return Ok(res);
        }
    }
}
