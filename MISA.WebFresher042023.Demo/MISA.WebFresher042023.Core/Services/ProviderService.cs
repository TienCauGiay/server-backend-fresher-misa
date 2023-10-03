using AutoMapper;
using Microsoft.AspNetCore.Http;
using MISA.WebFresher042023.Core.DTO.Accounts;
using MISA.WebFresher042023.Core.DTO.Employees;
using MISA.WebFresher042023.Core.DTO.Providers;
using MISA.WebFresher042023.Core.DTO.Receipts;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Enums;
using MISA.WebFresher042023.Core.Exceptions;
using MISA.WebFresher042023.Core.Interfaces.Infrastructures;
using MISA.WebFresher042023.Core.Interfaces.Services;
using MISA.WebFresher042023.Core.Interfaces.UnitOfWork;
using MISA.WebFresher042023.Core.Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Services
{
    public class ProviderService : BaseService<Provider, ProviderDto, ProviderCreateDto, ProviderUpdateDto>, IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IGroupProviderRepository _groupProviderRepository;
        private readonly IAccountProviderRepository _accountProviderRepository;
        private readonly IDeliveryAddressRepository _deliveryAddressRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProviderService(IUnitOfWork unitOfWork, IProviderRepository providerRepository, IMapper mapper,
            IGroupProviderRepository groupProviderRepository, IAccountProviderRepository accountProviderRepository,
            IDeliveryAddressRepository deliveryAddressRepository, IEmployeeRepository employeeRepository, 
            IGroupRepository groupRepository) : base(unitOfWork, providerRepository, mapper)
        {
            _unitOfWork = unitOfWork;
            _providerRepository = providerRepository;
            _groupProviderRepository = groupProviderRepository;
            _accountProviderRepository = accountProviderRepository;
            _deliveryAddressRepository = deliveryAddressRepository;
            _employeeRepository = employeeRepository;
            _groupRepository = groupRepository;
        }

        /// <summary>
        /// Tìm kiếm phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        public async Task<FilterProviderDto?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch)
        {
            var filterProvider = await _providerRepository.GetFilterAsync(pageSize, pageNumber, textSearch);
            if (filterProvider?.Data != null)
            {
                var filterProviderDto = _mapper.Map<FilterProviderDto>(filterProvider);
                return filterProviderDto;
            }
            return null;
        }

        /// <summary>
        /// Lấy mã nhà cung cấp lớn nhất trong hệ thống
        /// </summary>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        public async Task<string?> GetByCodeMaxAsync()
        {
            var codeMax = await _providerRepository.GetByCodeMaxAsync();
            if (codeMax != null)
            {
                var maxLength = codeMax.Length - 4;
                var maxProviderCode = long.Parse(codeMax.Substring(4)) + 1;
                var newProviderCode = $"NCC-{maxProviderCode.ToString().PadLeft(maxLength, '0')}";
                return newProviderCode;
            }
            return "";
        }

        /// <summary>
        /// Thêm 1 nhà cung cấp
        /// </summary>
        /// <param name="providerCreateDto"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (14/08/2023)
        public override async Task<int> InsertAsync(ProviderCreateDto providerCreateDto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                // Kiểm tra mã nhà cung cấp đã tồn tại hay chưa
                var checkDuplicateCode = await _providerRepository.GetByCodeAsync(providerCreateDto.ProviderCode);
                if (checkDuplicateCode != null)
                {
                    throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "ProviderCode", ProviderVN.ErrorLogic_Exist_ProviderCode } });
                }

                // Kiểm tra các nhóm có tồn tại không, nếu có mới cho thêm
                if (providerCreateDto.GroupIds != null && providerCreateDto.GroupIds.Count > 0)
                {
                    var groups = await _groupRepository.GetByListId(providerCreateDto.GroupIds);
                    if(groups == null || groups.Count != providerCreateDto.GroupIds.Count)
                    {
                        throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "GroupId", ProviderVN.Validate_NotExist_Group } });
                    }
                }

                // Kiểm tra nhân viên mua hàng có tồn tại hay không, nếu có mới cho thêm
                if (!string.IsNullOrEmpty(providerCreateDto.EmployeeId))
                {
                    var employee = await _employeeRepository.GetByIdAsync(new Guid(providerCreateDto.EmployeeId));
                    if (employee == null)
                    {
                        throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "EmployeeId", ReceiptVN.ErrorLogic_Exist_EmployeeId } });
                    }
                }

                var provider = _mapper.Map<Provider>(providerCreateDto);
                //Thêm nhà cung cấp
                var rowsAffected = await _providerRepository.InsertAsync(provider);

                var propviderInserted = await _providerRepository.GetByCodeAsync(provider.ProviderCode);

                if (propviderInserted != null)
                {
                    // Thêm nhóm nhà cung cấp
                    if (providerCreateDto.GroupIds != null && providerCreateDto.GroupIds.Count > 0)
                    {
                        List<GroupProvider> groupProviders = new List<GroupProvider>();
                        foreach (var groupId in providerCreateDto.GroupIds)
                        {
                            GroupProvider groupProvider = new GroupProvider();
                            groupProvider.GroupId = groupId;
                            groupProvider.ProviderId = propviderInserted.ProviderId;
                            groupProviders.Add(groupProvider);
                        }
                        await _groupProviderRepository.InsertMultipleAsync(groupProviders);
                    }

                    // Thêm tài khoản ngân hàng
                    if (providerCreateDto.AccountProviders != null && providerCreateDto.AccountProviders.Count > 0)
                    {
                        foreach (var accountProvider in providerCreateDto.AccountProviders)
                        {
                            accountProvider.AccountProviderId = Guid.NewGuid();
                            accountProvider.ProviderId = propviderInserted.ProviderId;
                            accountProvider.Flag = null;
                        }
                        await _accountProviderRepository.InsertMultipleAsync(providerCreateDto.AccountProviders);
                    }

                    // Thêm địa chỉ giao hàng
                    if (providerCreateDto.DeliveryAddresses != null && providerCreateDto.DeliveryAddresses.Count > 0)
                    {
                        foreach (var deliveryAddress in providerCreateDto.DeliveryAddresses)
                        {
                            deliveryAddress.DeliveryAddressId = Guid.NewGuid();
                            deliveryAddress.ProviderId = propviderInserted.ProviderId;
                            deliveryAddress.Flag = null;
                        }
                        await _deliveryAddressRepository.InsertMultipleAsync(providerCreateDto.DeliveryAddresses);
                    }

                }

                await _unitOfWork.CommitAsync();
                return rowsAffected;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Cập nhật thông tin tài khoản ngân hàng
        /// </summary>
        /// <param name="accountProviders"></param>
        /// <param name="providerId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (09/08/2023)
        private async Task UpdateAccountProviderAsync(List<AccountProvider>? accountProviders, Guid providerId)
        {
            if (accountProviders != null && accountProviders.Count > 0)
            {
                // Thêm
                var insertAccountProvider = accountProviders
                    .Where(x => x.Flag == E_Flag.Add)
                    .Select(accountProvider =>
                    {
                        accountProvider.AccountProviderId = Guid.NewGuid();
                        accountProvider.ProviderId = providerId;
                        accountProvider.Flag = null;
                        return accountProvider;
                    })
                    .ToList();
                if (insertAccountProvider != null && insertAccountProvider.Count > 0)
                {
                    await _accountProviderRepository.InsertMultipleAsync(insertAccountProvider);
                }

                // Sửa
                var updateAccountProvider = accountProviders
                    .Where(x => x.Flag == E_Flag.Update)
                    .Select(accountProvider =>
                    {
                        accountProvider.Flag = null;
                        return accountProvider;
                    })
                    .ToList();
                if (updateAccountProvider != null && updateAccountProvider.Count > 0)
                {
                    await _accountProviderRepository.UpdateMultipleAsync(updateAccountProvider);
                }

                // Xóa
                var deleteAccountProviderId = accountProviders.Where(x => x.Flag == E_Flag.Delete).Select(x => x.AccountProviderId).ToList();
                if (deleteAccountProviderId != null && deleteAccountProviderId.Count > 0)
                {
                    await _accountProviderRepository.DeleteMultipleAsync(deleteAccountProviderId);
                }
            }
        }

        /// <summary>
        /// Cập nhật thông tin địa chỉ giao hàng
        /// </summary>
        /// <param name="deliveryAddresses"></param>
        /// <param name="providerId"></param>
        /// <returns></returns>
        ///  Created By: BNTIEN (09/08/2023)
        private async Task UpdateDeliveryAddressAsync(List<DeliveryAddress>? deliveryAddresses, Guid providerId)
        {
            if (deliveryAddresses != null && deliveryAddresses.Count > 0)
            {
                // Thêm
                var insertDeliveryAddress = deliveryAddresses
                    .Where(x => x.Flag == E_Flag.Add)
                    .Select(deliveryAddress =>
                    {
                        deliveryAddress.DeliveryAddressId = Guid.NewGuid();
                        deliveryAddress.ProviderId = providerId;
                        deliveryAddress.Flag = null;
                        return deliveryAddress;
                    })
                    .ToList();
                if (insertDeliveryAddress != null && insertDeliveryAddress.Count > 0)
                {
                    await _deliveryAddressRepository.InsertMultipleAsync(insertDeliveryAddress);
                }

                // Sửa
                var updateDeliveryAddress = deliveryAddresses
                    .Where(x => x.Flag == E_Flag.Update)
                    .Select(deliveryAddress =>
                    {
                        deliveryAddress.Flag = null;
                        return deliveryAddress;
                    })
                    .ToList();
                if (updateDeliveryAddress != null && updateDeliveryAddress.Count > 0)
                {
                    await _deliveryAddressRepository.UpdateMultipleAsync(updateDeliveryAddress);
                }

                // Xóa
                var deleteDeliveryAddressId = deliveryAddresses.Where(x => x.Flag == E_Flag.Delete).Select(x => x.DeliveryAddressId).ToList();
                if (deleteDeliveryAddressId != null && deleteDeliveryAddressId.Count > 0)
                {
                    await _deliveryAddressRepository.DeleteMultipleAsync(deleteDeliveryAddressId);
                }
            }
        }

        /// <summary>
        /// Cập nhật thông tin nhà cung cấp
        /// </summary>
        /// <param name="providerUpdateDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        ///  Created By: BNTIEN (09/08/2023)
        public override async Task<int> UpdateAsync(ProviderUpdateDto providerUpdateDto, Guid id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                // Kiểm tra id đang được sửa có trùng khớp không
                if (providerUpdateDto.ProviderId != id)
                {
                    throw new ValidateException(StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_NotMatch, null);
                }

                // Kiểm tra mã nhà cung cấp đã tồn tại hay chưa
                var checkDuplicateCode = await _providerRepository.GetByCodeAsync(providerUpdateDto.ProviderCode);
                if (checkDuplicateCode != null)
                {
                    // Nếu tồn tại nhưng khác với mã nhà cung cấp của nhà cung cấp đang sửa
                    if (checkDuplicateCode.ProviderCode != (await _providerRepository.GetByIdAsync(id))?.ProviderCode)
                    {
                        throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "ProviderCode", ProviderVN.ErrorLogic_Exist_ProviderCode } });
                    }
                }

                // Kiểm tra các nhóm có tồn tại không, nếu có mới cho thêm
                if (providerUpdateDto.GroupIds != null && providerUpdateDto.GroupIds.Count > 0)
                {
                    var groups = await _groupRepository.GetByListId(providerUpdateDto.GroupIds);
                    if (groups == null || groups.Count != providerUpdateDto.GroupIds.Count)
                    {
                        throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "GroupId", ProviderVN.Validate_NotExist_Group } });
                    }
                }

                // Kiểm tra nhân viên mua hàng có tồn tại hay không, nếu có mới cho thêm
                if (!string.IsNullOrEmpty(providerUpdateDto.EmployeeId))
                {
                    var employee = await _employeeRepository.GetByIdAsync(new Guid(providerUpdateDto.EmployeeId));
                    if (employee == null)
                    {
                        throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "EmployeeId", ReceiptVN.ErrorLogic_Exist_EmployeeId } });
                    }
                }

                // Cập nhật thông tin nhà cung cấp
                var provider = _mapper.Map<Provider>(providerUpdateDto);
                var rowsAffected = await _providerRepository.UpdateAsync(provider, id);

                // Cập nhật thông tin nhóm nhà cung cấp
                // Lấy danh sách các nhóm nhà cung cấp có ProviderId = id và GroupId đã nằm trong groupIds
                var groupProviderExist = await _groupProviderRepository.GetExistAsync(id, providerUpdateDto.GroupIds);

                // Lọc để lấy danh sách những groupId chưa tồn tại
                var newGroupIds = providerUpdateDto.GroupIds?.Except(groupProviderExist.Select(gp => gp.GroupId)).ToList();
                // Thêm các nhóm nhà cung cấp mới
                if (newGroupIds != null && newGroupIds.Count > 0)
                {
                    List<GroupProvider> groupProviders = new List<GroupProvider>();
                    foreach (var groupId in newGroupIds)
                    {
                        GroupProvider groupProvider = new GroupProvider();
                        groupProvider.GroupId = groupId;
                        groupProvider.ProviderId = id;
                        groupProviders.Add(groupProvider);
                    }
                    await _groupProviderRepository.InsertMultipleAsync(groupProviders);
                }

                // Xóa các nhóm nhà cung cấp có ProviderId = id và GroupId không nằm trong groupIds
                await _groupProviderRepository.DeleteNotExistAsync(id, providerUpdateDto.GroupIds);

                // Cập nhật thông tin tài khoản ngân hàng 
                await UpdateAccountProviderAsync(providerUpdateDto.AccountProviders, id);

                // Cập nhật thông tin địa chỉ giao hàng
                await UpdateDeliveryAddressAsync(providerUpdateDto.DeliveryAddresses, id);

                await _unitOfWork.CommitAsync();
                return rowsAffected;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Xóa 1 nhà cung cấp theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///  Created By: BNTIEN (09/08/2023)
        public override async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                // Xóa các nhóm nhà cung cấp có mã nhà cung cấp là id
                await _groupProviderRepository.DeleteAsync(id);

                // Xóa các tài khoản ngân hàng
                await _accountProviderRepository.DeleteAsync(id);

                // Xóa địa chỉ giao hàng
                await _deliveryAddressRepository.DeleteAsync(id);

                // Xóa nhà cung cấp
                await base.DeleteAsync(id);

                await _unitOfWork.CommitAsync();
                return 1;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Xóa nhiều nhà cung cấp
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (09/08/2023)
        public override async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                // Xóa các nhóm nhà cung cấp có mã nhà cung cấp là id
                await _groupProviderRepository.DeleteMultipleAsync(ids);

                // Xóa các tài khoản ngân hàng
                await _accountProviderRepository.DeleteMultipleByProviderId(ids);

                // Xóa địa chỉ giao hàng
                await _deliveryAddressRepository.DeleteMultipleByProviderId(ids);
                // Xóa nhà cung cấp
                var rowsAffected = await base.DeleteMultipleAsync(ids);

                await _unitOfWork.CommitAsync();
                return rowsAffected;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Xuất excel
        /// </summary>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (09/08/2023)
        public async Task<MemoryStream> ExportExcelAsync(string? textSearch)
        {
            var providers = await _providerRepository.GetExportAsync(textSearch);

            var res = await _providerRepository.ExportExcelAsync(providers);
            return res;
        }
    }
}
