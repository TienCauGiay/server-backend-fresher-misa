using AutoMapper;
using Microsoft.AspNetCore.Http;
using MISA.WebFresher042023.Core.DTO.Accounts;
using MISA.WebFresher042023.Core.DTO.Employees;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Enums;
using MISA.WebFresher042023.Core.Exceptions;
using MISA.WebFresher042023.Core.Interfaces.Infrastructures;
using MISA.WebFresher042023.Core.Interfaces.Services;
using MISA.WebFresher042023.Core.Interfaces.UnitOfWork;
using MISA.WebFresher042023.Core.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.Services
{
    /// <summary>
    /// Class triển khai các phương thức của entities account
    /// </summary>
    /// Created By: BNTIEN (19/07/2023)
    public class AccountService
        : BaseService<Account, AccountDto, AccountCreateDto, AccountUpdateDto>, IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountantRepository _accountantRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AccountService(IUnitOfWork unitOfWork, IAccountRepository accountRepository, IAccountantRepository accountantRepository, IMapper mapper) : base(unitOfWork, accountRepository, mapper)
        {
            _accountRepository = accountRepository;
            _accountantRepository = accountantRepository;
            _unitOfWork = unitOfWork;
        }

        #region Method riêng (Employee)

        /// <summary>
        /// Tìm kiếm và phân trang trên giao diện
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns>Danh sách tài khoản theo tìm kiếm, phân trang</returns>
        /// Created By: BNTIEN (19/07/2023)
        public async Task<FilterAccountDto?> GetFilterAsync(int pageSize, int pageNumber, string? textSearch)
        {
            var filterAccount = await _accountRepository.GetFilterAsync(pageSize, pageNumber, textSearch);
            if (filterAccount?.Data != null)
            {
                var filterAccountDto = _mapper.Map<FilterAccountDto>(filterAccount);
                return filterAccountDto;
            }
            return null;
        }

        /// <summary>
        /// Lấy danh sách tất cả các con của tài khoản có số tài khoản là tham số truyền vào
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (26/07/2023)
        public async Task<List<AccountDto>?> GetAllChildrenAsync(string parentId)
        {
            var accountChildrens = await _accountRepository.GetAllChildrenAsync(parentId);
            if (accountChildrens != null)
            {
                var accountChildrensDto = _mapper.Map<List<AccountDto>>(accountChildrens);
                return accountChildrensDto;
            }
            return null;
        }

        /// <summary>
        /// Lấy danh sách tài khoản tổng hợp
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (26/07/2023)
        public async Task<FilterAccountDto?> GetBySearchFilterAsync(int pageSize, int pageNumber, string? textSearch)
        {
            var filterAccount = await _accountRepository.GetBySearchFilterAsync(pageSize, pageNumber, textSearch);
            if (filterAccount?.Data != null)
            {
                var filterAccountDto = _mapper.Map<FilterAccountDto>(filterAccount);
                return filterAccountDto;
            }
            return null;
        }

        /// <summary>
        /// Lấy danh sách tài khoản theo đối tượng (đổi tên cột) thích hợp
        /// </summary>
        /// <typeparam name="TAccount"></typeparam>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="textSearch"></param>
        /// <param name="storeProcedureName"></param>
        /// <returns>Danh sách tài khoản</returns
        /// Created By: BNTIEN (26/07/2023)
        public async Task<List<TAccount>> GetObjectAsync<TAccount>(int pageSize, int pageNumber, string? textSearch, string storeProcedureName)
        {
            var res = await _accountRepository.GetObjectAsync<TAccount>(pageSize, pageNumber, textSearch, storeProcedureName);
            return res;
        }

        /// <summary>
        /// Lấy danh sách khi chọn chức năng mở rộng trên giao diện
        /// </summary>
        /// <param name="account"></param>
        /// <returns>Danh sách tài khoản</returns>
        /// Created By: BNTIEN (26/07/2023)
        public async Task<List<Account>?> GetExpandAsync(int pageSize, int pageNumber, string? textSearch)
        {
            // Lấy các node gốc trên dataTable
            var listRoot = await _accountRepository.GetFilterAsync(pageSize, pageNumber, textSearch);

            if (listRoot?.Data != null && listRoot.Data.Count > 0)
            {
                var listAccountNumber = listRoot.Data.Select(account => account.AccountNumber).ToList();
                var res = await _accountRepository.GetExpandAsync(listAccountNumber);
                return res;
            }
            return null;
        }

        /// <summary>
        /// Tìm kiếm tải khoản
        /// </summary>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (26/07/2023)
        public async Task<FilterAccountDto?> GetBySearchAsync(int pageSize, int pageNumber, string? textSearch)
        {
            // Lấy danh sách tài khoản theo điều kiện tìm kiếm
            var accountByTextSearch = await _accountRepository.GetBySearchAsync(textSearch);
            if (accountByTextSearch != null && accountByTextSearch.Count > 0)
            {
                // Biến lưu danh sách số tài khoản
                var listAccountNumber = new List<string>();
                foreach (var account in accountByTextSearch)
                {
                    // Nếu lenght số tài khoản lớn hơn 3
                    if (account.AccountNumber.Length > 3)
                    {
                        for (int i = 3; i <= account.AccountNumber.Length; i++)
                        {
                            string accountNumber = account.AccountNumber.Substring(0, i);
                            listAccountNumber.Add(accountNumber);
                        }
                    }
                    else if (account.AccountNumber.Length == 3)
                    {
                        listAccountNumber.Add(account.AccountNumber);
                    }
                }

                // Loại bỏ các số tài khoản trùng lặp
                var uniqueAccountNubers = listAccountNumber.Distinct().ToList();

                // Lấy danh sách tài khoản theo list số tài khoản
                var accounts = await _accountRepository.GetByListCodeAsync(uniqueAccountNubers);

                // Phân trang
                // Lấy danh sách số tài khoản gốc
                var rootAccountNumbers = accounts.Where(account => account.IsRoot == BoolNumber.True).Select(account => account.AccountNumber).ToList();

                // Tạo danh sách các số tài khoản gốc được phân trang
                var pagedRootAccountNumbers = rootAccountNumbers.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

                // Lọc danh sách tài khoản theo các số tài khoản gốc phân trang
                var pagedAccounts = accounts.Where(account => pagedRootAccountNumbers.Any(rootNumber => account.AccountNumber.StartsWith(rootNumber))).ToList();

                // Chuyển đổi danh sách tài khoản sang DTO nếu cần
                var accountDtos = _mapper.Map<List<AccountDto>>(pagedAccounts);

                return new FilterAccountDto
                {
                    TotalPage = (int)Math.Ceiling((decimal)rootAccountNumbers.Count / pageSize),
                    TotalRecord = accounts.Count,
                    CurrentPage = pageNumber,
                    CurrentPageRecords = accountDtos.Count,
                    Data = accountDtos,
                };
            }
            return new FilterAccountDto();
        }

        /// <summary>
        /// Validate chung cho insert và update
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="parentId"></param>
        /// <exception cref="ValidateException"></exception>
        private void ValidateAccountCommon(string accountNumber, string? parentId)
        {
            // Nếu độ dài số tài khoản lớn hơn 3 thì phải nhập tài khoản tổng hợp
            if (accountNumber.Length > 3 && string.IsNullOrEmpty(parentId))
            {
                throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "ParentId", AccountVN.ErrorLogic_Fail_ParentId } });
            }
        }

        /// <summary>
        /// Thêm 1 tài khoản mới
        /// </summary>
        /// <param name="accountCreateDto"></param>
        /// <returns>Số hàng ảnh hưởng sau khi thêm</returns>
        /// <exception cref="ValidateException">Bắn ra lỗi nếu dữ liệu đầu vào không hợp lệ</exception>
        /// Created By: BNTIEN (20/07/2023)
        public override async Task<int> InsertAsync(AccountCreateDto accountCreateDto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                // validate chung
                ValidateAccountCommon(accountCreateDto.AccountNumber, accountCreateDto.ParentId);

                // Kiểm tra số tài khoản đã tồn tại hay chưa
                var checkDuplicateAccountNumber = await _accountRepository.GetByCodeAsync(accountCreateDto.AccountNumber);
                if (checkDuplicateAccountNumber != null)
                {
                    throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "AccountNumber", AccountVN.ErrorLogic_Exist_AccountNumber } });
                }

                // Nếu không có tài khoản tổng hợp, tài khoản được thêm mới sẽ là gốc
                if (string.IsNullOrEmpty(accountCreateDto.ParentId))
                {
                    accountCreateDto.Grade = 1;
                    accountCreateDto.IsRoot = BoolNumber.True;
                }
                else
                {
                    // Kiểm tra xem tài khoản tổng hợp đã tồn tại hay chưa đã tồn tại không, nếu có mới thêm
                    var checkParentExist = await _accountRepository.GetByIdAsync(new Guid(accountCreateDto.ParentId));
                    if (checkParentExist == null)
                    {
                        throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "ParentId", AccountVN.Validate_NotExist_ParentId } });
                    }
                    else
                    {
                        // Nếu tài khoản tổng hợp tồn tại nhưng đang ở trạng thái ngưng sử dụng
                        if (checkParentExist.State == BoolNumber.False)
                        {
                            throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "ParentId", $"{AccountVN.Text_Account} <{checkParentExist.AccountNumber}> {AccountVN.Validate_UsingState_ParentId}" } });
                        }
                    }

                    // Nếu số tài khoản của tài khoản insert không bắt đầu từ số tài khoản của tài khoản tổng hợp
                    if (!accountCreateDto.AccountNumber.StartsWith(checkParentExist.AccountNumber))
                    {
                        throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "AccountNumber", AccountVN.InValid_ParentId } });
                    }

                    // Kiểm tra nếu số tài khoản đang thêm chứa 1 số tài khoản cùng cấp thì không cho thêm
                    var sameAccounts = await _accountRepository.GetAllChildrenAsync(accountCreateDto.ParentId);
                    if(sameAccounts != null && sameAccounts.Count > 0)
                    {
                        foreach (var account in sameAccounts)
                        {
                            // Phải lấy phần trùng nên không if chung được
                            if(account.AccountNumber.StartsWith(accountCreateDto.AccountNumber))
                            {
                                throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "AccountNumber", string.Format(AccountVN.Validate_ExitStartWith_AccountNumber, accountCreateDto.AccountNumber, account.AccountNumber) } });
                            }
                            if (accountCreateDto.AccountNumber.StartsWith(account.AccountNumber))
                            {
                                throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "AccountNumber", string.Format(AccountVN.Validate_ExitStartWith_AccountNumber, account.AccountNumber, account.AccountNumber) } });
                            }
                        }
                    }                 

                    accountCreateDto.Grade = checkParentExist.Grade + 1;
                    accountCreateDto.IsRoot = BoolNumber.False;

                    // Cập nhật trạng thái IsParent cho tài khoản tổng hợp nếu chưa là cha => cha
                    if (checkParentExist.IsParent == BoolNumber.False)
                    {
                        checkParentExist.IsParent = BoolNumber.True;
                        await _accountRepository.UpdateAsync(checkParentExist, checkParentExist.AccountId);
                    }
                }

                // set giá trị mặc định khi mới thêm 1 tài khoản mới
                accountCreateDto.IsParent = BoolNumber.False;
                accountCreateDto.State = BoolNumber.True;

                var res = await base.InsertAsync(accountCreateDto);
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
        /// Cập nhật thông tin 1 tài khoản
        /// </summary>
        /// <param name="accountUpdateDto"></param>
        /// <param name="id"></param>
        /// <returns>Số hàng ảnh hưởng sau khi cập nhật</returns>
        /// Created By: BNTIEN (20/07/2023)
        public override async Task<int> UpdateAsync(AccountUpdateDto accountUpdateDto, Guid id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                if (accountUpdateDto.AccountId != id)
                {
                    throw new ValidateException(StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_NotMatch, null);
                }

                // validate chung
                ValidateAccountCommon(accountUpdateDto.AccountNumber, accountUpdateDto.ParentId);

                // Kiểm tra số tài khoản đã tồn tại hay chưa
                var checkDuplicateAccountNumber = await _accountRepository.GetByCodeAsync(accountUpdateDto.AccountNumber);
                if (checkDuplicateAccountNumber != null)
                {
                    // Nếu tồn tại nhưng khác với số tài khoản của tài khoản đang sửa
                    if (checkDuplicateAccountNumber.AccountNumber != (await _accountRepository.GetByIdAsync(id))?.AccountNumber)
                    {
                        throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "AccountNumber", AccountVN.ErrorLogic_Exist_AccountNumber } });
                    }
                }

                // Lấy tài khoản trong database theo id (thông tin trước khi sửa)
                var accountById = await _accountRepository.GetByIdAsync(id);

                // Nếu tài khoản có liên quan đến hạch toán mà đã sửa số tài khoản thì báo lỗi, không cho sửa
                var accountByAccountant = await _accountantRepository.GetByAccountIdAsync(id);
                if (accountByAccountant != null && accountById?.AccountNumber.Trim() != accountUpdateDto.AccountNumber.Trim())
                {
                    throw new ValidateException(StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "AccountNumber", string.Format(AccountVN.ErrorLogic_Update_Account, accountById?.AccountNumber) } });
                }

                if (accountUpdateDto.IsParent == BoolNumber.True)
                {
                    // Nếu tài khoản được sửa là cha và đã thay đổi thông tin tài khoản tổng hợp hoặc số tài khoản
                    if (accountById?.ParentId?.Trim() != accountUpdateDto.ParentId?.Trim() || accountById?.AccountNumber.Trim() != accountUpdateDto.AccountNumber.Trim())
                    {
                        throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "AccountNumber", AccountVN.ErrorLogic_Constraint_AccountNumber } });
                    }
                }
                else if (accountUpdateDto.IsParent == BoolNumber.False)
                {
                    // Nếu không có tài khoản tổng hợp, tài khoản được sửa mới sẽ là gốc
                    if (string.IsNullOrEmpty(accountUpdateDto.ParentId))
                    {
                        accountUpdateDto.Grade = 1;
                        accountUpdateDto.IsRoot = BoolNumber.True;
                    }
                    else
                    {
                        // Kiểm tra xem tài khoản tổng hợp đã tồn tại hay chưa đã tồn tại không, nếu có mới sửa
                        var checkParentExist = await _accountRepository.GetByIdAsync(new Guid(accountUpdateDto.ParentId));
                        if (checkParentExist == null)
                        {
                            throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "ParentId", AccountVN.Validate_NotExist_ParentId } });
                        }
                        else
                        {
                            // Nếu tài khoản tổng hợp tồn tại nhưng đang ở trạng thái ngưng sử dụng
                            if (checkParentExist.State == BoolNumber.False)
                            {
                                throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "ParentId", $"{AccountVN.Text_Account} <{checkParentExist.AccountNumber}> {AccountVN.Validate_UsingState_ParentId}" } });
                            }
                        }

                        // Nếu số tài khoản của tài khoản update không bắt đầu từ số tài khoản của tài khoản tổng hợp
                        if (!accountUpdateDto.AccountNumber.StartsWith(checkParentExist.AccountNumber))
                        {
                            throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "AccountNumber", AccountVN.InValid_ParentId } });
                        }


                        // Kiểm tra nếu số tài khoản đang sửa chứa 1 số tài khoản cùng cấp mà khác nó thì không cho thêm
                        var accountChildrens = await _accountRepository.GetAllChildrenAsync(accountUpdateDto.ParentId);
                        var sameAccounts = accountChildrens?.Where(account => account.AccountNumber.Trim() != accountById?.AccountNumber.Trim()).ToList();
                        if (sameAccounts != null && sameAccounts.Count > 0)
                        {
                            foreach (var account in sameAccounts)
                            {
                                // Phải lấy phần trùng nên không if chung được
                                if (account.AccountNumber.StartsWith(accountUpdateDto.AccountNumber))
                                {
                                    throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "AccountNumber", string.Format(AccountVN.Validate_ExitStartWith_AccountNumber, accountUpdateDto.AccountNumber, account.AccountNumber) } });
                                }
                                if (accountUpdateDto.AccountNumber.StartsWith(account.AccountNumber))
                                {
                                    throw new ValidateException(errorCode: StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new Dictionary<string, string> { { "AccountNumber", string.Format(AccountVN.Validate_ExitStartWith_AccountNumber, account.AccountNumber, account.AccountNumber) } });
                                }
                            }
                        }

                        // Cập nhật cho node cha mà thằng update chuyển đến trạng thái IsParent
                        if (checkParentExist.IsParent == BoolNumber.False)
                        {
                            checkParentExist.IsParent = BoolNumber.True;
                            await _accountRepository.UpdateAsync(checkParentExist, checkParentExist.AccountId);
                        }

                        // Cập nhật trạng thái IsParent cho node cha mà thằng update rời đi
                        var (parentAccount, countChildren) = await _accountRepository.GetCountChildren(accountById?.ParentId);
                        if (countChildren < 2 && parentAccount != null && accountUpdateDto.ParentId.Trim() != accountById.ParentId.Trim())
                        {
                            parentAccount.IsParent = BoolNumber.False;
                            await _accountRepository.UpdateAsync(parentAccount, parentAccount.AccountId);
                        }

                        accountUpdateDto.Grade = checkParentExist.Grade + 1;
                        accountUpdateDto.IsRoot = BoolNumber.False;
                    }
                }

                var res = await base.UpdateAsync(accountUpdateDto, id);
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
        /// Cập nhật trạng thái tài khoản
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <param name="state"></param>
        /// <param name="isUpdateChildren"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (26/07/2023)
        public async Task<int> UpdateStateAsync(AccountUpdateDto account, int state, int isUpdateChildren)
        {
            // Nếu đang từ trạng thái ngưng sử dụng chuyển sang sử dụng
            if (account.State == BoolNumber.False && !string.IsNullOrEmpty(account.ParentId))
            {
                // Kiểm tra nếu cha đang ở trạng thái ngưng sử dụng thì không cho update
                var accountParent = await _accountRepository.GetByIdAsync(new Guid(account.ParentId));
                if (accountParent != null && accountParent.State == BoolNumber.False)
                {
                    throw new ValidateException(StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new { Data = AccountVN.ErrorLogic_NotUsing_Account });
                }
            }
            var res = await _accountRepository.UpdateStateAsync(account.AccountNumber, state, isUpdateChildren);
            return res;
        }

        /// <summary>
        /// Xóa 1 tài khoản
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<int> DeleteAsync(Guid id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                // Nếu tài khoản là cha thì không cho xóa
                var accountDelete = await _accountRepository.GetByIdAsync(id);
                if (accountDelete?.IsParent == BoolNumber.True)
                {
                    throw new ValidateException(StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new { Data = AccountVN.ErrorLogic_NotDelete_Account_1 });
                }

                // Nếu tài khoản có liên quan đến hạch toán thì không cho xóa
                var accountByAccountant = await _accountantRepository.GetByAccountIdAsync(id);
                if (accountByAccountant != null)
                {
                    throw new ValidateException(StatusCodes.Status400BadRequest, Resources.ResourceVN.Validate_User_Input_Error, new { Data = AccountVN.ErrorLogic_NotDelete_Account_2 });
                }

                // Cập nhật trạng thái IsParent cho node cha có thằng con bị xóa
                var (parentAccount, countChildren) = await _accountRepository.GetCountChildren(accountDelete.ParentId);
                if (countChildren < 2 && parentAccount != null)
                {
                    parentAccount.IsParent = BoolNumber.False;
                    await _accountRepository.UpdateAsync(parentAccount, parentAccount.AccountId);
                }

                var res = await base.DeleteAsync(id);
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
        /// Xuất excel
        /// </summary>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        /// Created By: BNTIEN (26/07/2023)
        public async Task<MemoryStream> ExportExcelAsync(string? textSearch)
        {
            var accounts = await _accountRepository.GetBySearchAsync(textSearch);

            var res = await _accountRepository.ExportExcelAsync(accounts);
            return res;
        }
        #endregion
    }
}
