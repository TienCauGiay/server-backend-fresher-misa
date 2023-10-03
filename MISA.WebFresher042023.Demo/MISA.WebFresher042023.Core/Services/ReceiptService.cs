using AutoMapper;
using Microsoft.AspNetCore.Http;
using MISA.WebFresher042023.Core.DTO.Accountants;
using MISA.WebFresher042023.Core.DTO.Accounts;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Services
{
    public class ReceiptService : BaseService<Receipt, ReceiptDto, ReceiptCreateDto, ReceiptUpdateDto>, IReceiptService
    {
        private readonly IReceiptRepository _receiptRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IProviderRepository _providerRepository;
        private readonly IAccountantRepository _accountantRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ReceiptService(IUnitOfWork unitOfWork, IReceiptRepository receiptRepository,
            IEmployeeRepository employeeRepository, IProviderRepository providerRepository,
            IAccountantRepository accountantRepository, IMapper mapper) : base(unitOfWork, receiptRepository, mapper)
        {
            _unitOfWork = unitOfWork;
            _receiptRepository = receiptRepository;
            _employeeRepository = employeeRepository;
            _providerRepository = providerRepository;
            _accountantRepository = accountantRepository;
        }

        /// <summary>
        /// Lấy mã phiếu chi lớn nhất trong hệ thống
        /// </summary>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        public async Task<string?> GetByCodeMaxAsync()
        {
            var codeMax = await _receiptRepository.GetByCodeMaxAsync();
            if (codeMax != null)
            {
                var maxLength = codeMax.Length - 2;
                var maxReceiptNumber = long.Parse(codeMax.Substring(2)) + 1;
                var newReceiptNumber = $"PC{maxReceiptNumber.ToString().PadLeft(maxLength, '0')}";
                return newReceiptNumber;
            }
            return "";
        }

        /// <summary>
        /// Tìm kiếm phân trang
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <param name="keyFilter"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        public async Task<FilterReceiptDto?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch, bool? keyFilter)
        {
            var filterReceipt = await _receiptRepository.GetFilterAsync(pageSize, pageNumber, textSearch, keyFilter);
            if (filterReceipt?.Data != null)
            {
                var filterReceiptDto = _mapper.Map<FilterReceiptDto>(filterReceipt);
                return filterReceiptDto;
            }
            return null;
        }

        /// <summary>
        /// Validate kiểm tra xem có ghi sổ được không
        /// </summary>
        /// <param name="accountants"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        private bool CheckIsNoted(List<AccountantDto> accountants)
        {
            foreach (var accountant in accountants)
            {
                // Nếu tài khoản hạch toán không theo nhà cung cấp
                if (accountant.UserObjectDebt == E_UserObjectAccount.Customer
                    || accountant.UserObjectDebt == E_UserObjectAccount.Employee ||
                    accountant.UserObjectBalance == E_UserObjectAccount.Customer ||
                    accountant.UserObjectBalance == E_UserObjectAccount.Employee)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// validate hạch toán
        /// </summary>
        /// <param name="accountants"></param>
        /// <exception cref="ValidateException"></exception>
        /// Created By: BNTIEN (16/08/2023)
        private void ValidateAccountant(List<AccountantDto> accountants)
        {
            foreach (var accountant in accountants)
            {
                if (string.IsNullOrEmpty(accountant.AccountDebtId.ToString()))
                {
                    throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "AccountantDebt", ReceiptVN.Validate_NotNull_Accountant } });
                }
                else if (string.IsNullOrEmpty(accountant.AccountBalanceId.ToString()))
                {
                    throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "AccountantBalance", ReceiptVN.Validate_NotNull_AccountBalance } });
                }
            }
        }

        /// <summary>
        /// override lại hàm thêm mới phiếu chi từ base để validate
        /// </summary>
        /// <param name="receiptCreateDto"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        public override async Task<int> InsertAsync(ReceiptCreateDto receiptCreateDto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                // validate ngày hạch toán phải lớn hơn hoặc bằng ngày chứng từ
                if (receiptCreateDto.ReceiptDate > receiptCreateDto.AccountingDate)
                {
                    throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "AccountingDate", ReceiptVN.ErrorLogic_Date_AccountingDate } });
                }

                // Kiểm tra xem số phiếu đã tồn tại hay chưa
                var checkDuplicateCode = await _receiptRepository.GetByCodeAsync(receiptCreateDto.ReceiptNumber);
                if (checkDuplicateCode != null)
                {
                    throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "ReceiptNumber", ReceiptVN.ErrorLogic_Exist_ReceiptNumber } });
                }

                // Kiểm tra mã nhà cung cấp có tồn tại không, nếu có mới thêm
                if (!string.IsNullOrEmpty(receiptCreateDto.ProviderId))
                {
                    var provider = await _providerRepository.GetByIdAsync(new Guid(receiptCreateDto.ProviderId));
                    if (provider == null)
                    {
                        throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "ProviderId", ReceiptVN.ErrorLogic_Exist_ProviderId } });
                    }
                }

                // Kiểm tra mã nhân viên có tồn tại không, nếu có mới thêm
                if (!string.IsNullOrEmpty(receiptCreateDto.EmployeeId))
                {
                    var employee = await _employeeRepository.GetByIdAsync(new Guid(receiptCreateDto.EmployeeId));
                    if (employee == null)
                    {
                        throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "EmployeeId", ReceiptVN.ErrorLogic_Exist_EmployeeId } });
                    }
                }

                // Kiểm tra trạng thái phiếu có được ghi sổ hay không
                if (receiptCreateDto.AccountantList != null && receiptCreateDto.AccountantList.Count > 0)
                {
                    if (string.IsNullOrEmpty(receiptCreateDto.ProviderId))
                    {
                        receiptCreateDto.IsNoted = false;
                    }
                    else
                    {
                        receiptCreateDto.IsNoted = CheckIsNoted(receiptCreateDto.AccountantList);
                    }
                }
                else // Nếu thiếu hạch toán 
                {
                    throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new { Data = ReceiptVN.Validate_NotNull_Accountant });
                }

                // Thêm phiếu thu/chi
                var receipt = _mapper.Map<Receipt>(receiptCreateDto);
                var res = await _receiptRepository.InsertAsync(receipt);

                // Validate hạch toán
                if (receiptCreateDto.AccountantList != null && receiptCreateDto.AccountantList.Count > 0)
                {
                    ValidateAccountant(receiptCreateDto.AccountantList);
                }

                // Thêm các hạch toán
                if (receiptCreateDto.AccountantList != null && receiptCreateDto.AccountantList.Count > 0)
                {
                    var receiptInserted = await _receiptRepository.GetByCodeAsync(receipt.ReceiptNumber);

                    if (receiptInserted != null)
                    {
                        // set dữ liệu cho các bản ghi
                        foreach (var accountant in receiptCreateDto.AccountantList)
                        {
                            accountant.AccountantId = Guid.NewGuid();
                            accountant.ReceiptId = receiptInserted.ReceiptId;
                        }

                        // Thêm hạch toán 
                        var accountantList = _mapper.Map<List<Accountant>>(receiptCreateDto.AccountantList);
                        await _accountantRepository.InsertMultipleAsync(accountantList);
                    }
                }

                await _unitOfWork.CommitAsync();
                return res;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Cập nhật nhiều hạch toán theo cờ
        /// </summary>
        /// <param name="accountants"></param>
        /// <param name="receiptId"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        private async Task UpdateMultipleAccountantAsync(List<AccountantDto> accountants, Guid receiptId)
        {
            // Thêm
            var insertAccountant = accountants
                .Where(x => x.Flag == E_Flag.Add)
                .Select(accountant =>
                {
                    accountant.AccountantId = Guid.NewGuid();
                    accountant.ReceiptId = receiptId;
                    accountant.Flag = null;
                    return accountant;
                })
                .ToList();
            if (insertAccountant != null && insertAccountant.Count > 0)
            {
                var insertAccountantList = _mapper.Map<List<Accountant>>(insertAccountant);
                await _accountantRepository.InsertMultipleAsync(insertAccountantList);
            }

            // Sửa
            var updateAccountant = accountants
                .Where(x => x.Flag == E_Flag.Update)
                .Select(accountant =>
                {
                    accountant.Flag = null;
                    return accountant;
                })
                .ToList();
            if (updateAccountant != null && updateAccountant.Count > 0)
            {
                var updateAccountantList = _mapper.Map<List<Accountant>>(updateAccountant);
                await _accountantRepository.UpdateMultipleAsync(updateAccountantList);
            }

            // Xóa
            var deleteAccountant = accountants.Where(x => x.Flag == E_Flag.Delete).Select(x => x.AccountantId).ToList();
            if (deleteAccountant != null && deleteAccountant.Count > 0)
            {
                await _accountantRepository.DeleteMultipleAsync(deleteAccountant);
            }
        }

        /// <summary>
        /// override hàm cập nhật phiếu chi từ base để validate
        /// </summary>
        /// <param name="receiptUpdateDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        public override async Task<int> UpdateAsync(ReceiptUpdateDto receiptUpdateDto, Guid id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                // validate ngày hạch toán phải lớn hơn hoặc bằng ngày chứng từ
                if (receiptUpdateDto.ReceiptDate > receiptUpdateDto.AccountingDate)
                {
                    throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "AccountingDate", ReceiptVN.ErrorLogic_Date_AccountingDate } });
                }

                // Kiểm tra số phiếu chi đã tồn tại hay chưa
                var checkDuplicateCode = await _receiptRepository.GetByCodeAsync(receiptUpdateDto.ReceiptNumber);
                if (checkDuplicateCode != null)
                {
                    // Nếu tồn tại nhưng khác với mã nhà cung cấp của nhà cung cấp đang sửa
                    if (checkDuplicateCode.ReceiptNumber != (await _receiptRepository.GetByIdAsync(id))?.ReceiptNumber)
                    {
                        throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "ReceiptNumber", ReceiptVN.ErrorLogic_Exist_ReceiptNumber } });
                    }
                }

                // Kiểm tra mã nhà cung cấp có tồn tại không, nếu có mới thêm
                if (!string.IsNullOrEmpty(receiptUpdateDto.ProviderId))
                {
                    var provider = await _providerRepository.GetByIdAsync(new Guid(receiptUpdateDto.ProviderId));
                    if (provider == null)
                    {
                        throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "ProviderId", ReceiptVN.ErrorLogic_Exist_ProviderId } });
                    }
                }

                // Kiểm tra mã nhân viên có tồn tại không, nếu có mới thêm
                if (!string.IsNullOrEmpty(receiptUpdateDto.EmployeeId))
                {
                    var employee = await _employeeRepository.GetByIdAsync(new Guid(receiptUpdateDto.EmployeeId));
                    if (employee == null)
                    {
                        throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "EmployeeId", ReceiptVN.ErrorLogic_Exist_EmployeeId } });
                    }
                }

                // Kiểm tra trạng thái phiếu có được ghi sổ hay không
                if (receiptUpdateDto.AccountantList != null && receiptUpdateDto.AccountantList.Count > 0)
                {
                    if (string.IsNullOrEmpty(receiptUpdateDto.ProviderId))
                    {
                        receiptUpdateDto.IsNoted = false;
                    }
                    else
                    {
                        receiptUpdateDto.IsNoted = CheckIsNoted(receiptUpdateDto.AccountantList);
                    }
                }
                else // Nếu thiếu hạch toán 
                {
                    throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new { Data = ReceiptVN.Validate_NotNull_Accountant });
                }

                // Cập nhật thông tin phiếu chi
                var receipt = _mapper.Map<Receipt>(receiptUpdateDto);
                var res = await _receiptRepository.UpdateAsync(receipt, id);

                // Validate hạch toán
                if (receiptUpdateDto.AccountantList != null && receiptUpdateDto.AccountantList.Count > 0)
                {
                    ValidateAccountant(receiptUpdateDto.AccountantList);
                }

                // Cập nhật thông tin các hạch toán
                if (receiptUpdateDto.AccountantList != null && receiptUpdateDto.AccountantList.Count > 0)
                {
                    await UpdateMultipleAccountantAsync(receiptUpdateDto.AccountantList, id);
                }

                await _unitOfWork.CommitAsync();
                return res;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Cập nhật trạng thái ghi sổ/ bỏ ghi 1 phiếu chi
        /// </summary>
        /// <param name="receiptUpdateDto"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        public async Task<int> UpdateNoteAsync(ReceiptUpdateDto receiptUpdateDto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var accountantList = await _accountantRepository.GetByReceiptIdAsync(receiptUpdateDto.ReceiptId);

                /// Kiểm tra xem các hạch toán có đủ điều kiện ghi sổ không
                if (accountantList == null || accountantList.Count == 0)
                {
                    // Nếu từ chưa ghi sổ => ghi sổ
                    if (receiptUpdateDto.IsNoted == false)
                    {
                        throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new { Data = ReceiptVN.Validate_NotNull_Accountant });
                    }
                }
                else
                {
                    // Nếu từ chưa ghi sổ => ghi sổ
                    if (receiptUpdateDto.IsNoted == false)
                    {
                        foreach (var accountant in accountantList)
                        {
                            // Nếu tài khoản nợ không theo nhà cung cấp
                            if (accountant.UserObjectDebt == E_UserObjectAccount.Customer || accountant.UserObjectDebt == E_UserObjectAccount.Employee)
                            {
                                throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new { Data = string.Format(ReceiptVN.ErrorLogic_NotFollowProvider_AccountDebt, accountant.AccountDebtNumber) });
                            }
                            else
                            {
                                // Nếu tài khoản nợ theo nhà cung cấp nhưng chưa chọn nhà cung cấp
                                if (string.IsNullOrEmpty(receiptUpdateDto.ProviderId))
                                {
                                    throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new { Data = string.Format(ReceiptVN.ErrorLogic_LackOfProvider_AccountDebt, accountant.AccountDebtNumber) });
                                }
                            }
                            // Nếu tài khoản có không theo nhà cung cấp
                            if (accountant.UserObjectBalance == E_UserObjectAccount.Customer || accountant.UserObjectBalance == E_UserObjectAccount.Employee)
                            {
                                throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new { Data = string.Format(ReceiptVN.ErrorLogic_NotFollowProvider_AccountDebt, accountant.AccountBalanceNumber) });
                            }
                            else
                            {
                                // Nếu tài khoản có theo nhà cung cấp nhưng chưa chọn nhà cung cấp
                                if (string.IsNullOrEmpty(receiptUpdateDto.ProviderId))
                                {
                                    throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new { Data = string.Format(ReceiptVN.ErrorLogic_LackOfProvider_AccountDebt, accountant.AccountBalanceNumber) });
                                }
                            }
                        }
                    }
                }

                var receipt = _mapper.Map<Receipt>(receiptUpdateDto);
                var res = await _receiptRepository.UpdateNoteAsync(receipt);

                await _unitOfWork.CommitAsync();
                return res;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Cập nhật trạng thái ghi sổ/bỏ ghi nhiều phiếu chi
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="typeUpdate"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        public async Task<int> UpdateNoteMultipleAsync(List<Guid> ids, bool typeUpdate)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                if (typeUpdate == true) // Nếu từ chưa ghi sổ => ghi sổ
                {
                    var satisfiedNote = await _receiptRepository.GetSatisfiedAsync(ids, ReceiptNote.UnNoted);

                    // Kiểm tra xem có đủ điều kiện ghi sổ hay không
                    // Validate (chức năng ghi sổ nhiều đang phát triển)

                    if (satisfiedNote != null && satisfiedNote.Count > 0)
                    {
                        var listIdUpdate = satisfiedNote.Select(x => x.ReceiptId).ToList();
                        var res = await _receiptRepository.UpdateNoteMultipleAsync(listIdUpdate, typeUpdate);
                        await _unitOfWork.CommitAsync();
                        return res;
                    }
                } 
                // Nếu từ ghi sổ => bỏ ghi
                var satisfiedUnNote = await _receiptRepository.GetSatisfiedAsync(ids, ReceiptNote.Noted);
                if (satisfiedUnNote != null && satisfiedUnNote.Count > 0)
                {
                    var listIdUpdate = satisfiedUnNote.Select(x => x.ReceiptId).ToList();
                    var res = await _receiptRepository.UpdateNoteMultipleAsync(listIdUpdate, typeUpdate);
                    await _unitOfWork.CommitAsync();
                    return res;
                }
                return 0;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Xóa 1 phiếu thu/chi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        public override async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                // Kiểm tra xem phiếu đã ghi sổ chưa, nêu ghi rồi thì không cho xóa
                var receiptDelete = await _receiptRepository.GetByIdAsync(id);
                if (receiptDelete != null)
                {
                    if (receiptDelete.IsNoted == true)
                    {
                        throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "ReceiptId", ReceiptVN.ErrorLogic_IsNoted } });
                    }

                    // Xóa hạch toán có receipt id
                    await _accountantRepository.DeleteAsync(id);

                    // Xóa phiếu
                    await base.DeleteAsync(id);

                    await _unitOfWork.CommitAsync();
                    return 1;
                }
                return 0;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Xóa nhiều phiếu thu/chi
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        public override async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                // Lấy các bản ghi thỏa mãn điều kiện xóa, tức là chưa ghi sổ
                var satisfiedDelete = await _receiptRepository.GetSatisfiedAsync(ids, ReceiptNote.UnNoted);
                var rowDelele = 0;
                if (satisfiedDelete != null && satisfiedDelete.Count > 0)
                {
                    var listIdDelete = satisfiedDelete.Select(x => x.ReceiptId).ToList();

                    // Xóa các hạch toán liên quan theo list id phiếu
                    await _accountantRepository.DeleteMultipleByReceiptIdAsync(listIdDelete);

                    // Xóa các phiếu theo list id
                    rowDelele = await base.DeleteMultipleAsync(listIdDelete);
                    await _unitOfWork.CommitAsync();
                }
                return rowDelele;
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
        /// <param name="keyFilter"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (16/08/2023)
        public async Task<MemoryStream> ExportExcelAsync(string? textSearch, bool? keyFilter)
        {
            var receipts = await _receiptRepository.GetExportAsync(textSearch, keyFilter);

            var res = await _receiptRepository.ExportExcelAsync(receipts);
            return res;
        }
    }
}
