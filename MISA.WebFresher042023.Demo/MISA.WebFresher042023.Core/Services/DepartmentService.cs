using AutoMapper;
using MISA.WebFresher042023.Core.DTO.Departments;
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
    /// Class triển khai các phương thức của entities department
    /// </summary>
    /// Created By: BNTIEN (17/06/2023)
    public class DepartmentService : BaseService<Department, DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>, IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentService(IUnitOfWork unitOfWork, IDepartmentRepository departmentRepository, IMapper mapper) : base(unitOfWork, departmentRepository, mapper)
        {
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        #region Method riêng (Department)
        /// <summary>
        /// Tìm kiếm phòng ban theo tên
        /// </summary>
        /// <param name="textSearch"></param>
        /// <returns>Danh sách phòng ban</returns>
        /// Created By: BNTIEN (17/06/2023)
        public async Task<IEnumerable<DepartmentDto>?> GetByName(string? textSearch)
        {
            var res = await _departmentRepository.GetByName(textSearch);
            if (res != null)
            {
                var resDto = _mapper.Map<List<DepartmentDto>>(res);
                return (IEnumerable<DepartmentDto>?)resDto;
            }
            return null;
        }
        #endregion
    }
}
