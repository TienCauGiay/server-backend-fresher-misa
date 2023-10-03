using MISA.WebFresher042023.Core.DTO.Employees.CustomValidate;
using MISA.WebFresher042023.Core.Entities;
using MISA.WebFresher042023.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Providers
{
    /// <summary>
    /// class update provider
    /// </summary>
    /// Created By: BNTIEN (13/08/2023)
    public class ProviderUpdateDto
    {
        /// <summary>
        /// Id nhà cung cấp (Khóa chính)
        /// </summary>
        public Guid ProviderId { get; set; }
        /// <summary>
        /// Mã nhà cung cấp
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_NotNull_ProviderCode))]
        [MaxLength(20, ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_MaxLength_ProviderCode))]
        public string ProviderCode { get; set; }

        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_NotNull_ProviderName))]
        [MaxLength(255, ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_MaxLength_ProviderName))]
        public string ProviderName { get; set; }

        /// <summary>
        /// Mã số thuế
        /// </summary>
        [MaxLength(20, ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_MaxLength_TaxCode))]
        public string? TaxCode { get; set; }

        /// <summary>
        /// Là cá nhân hay không
        /// </summary>
        public bool IsPersonal { get; set; }

        /// <summary>
        /// Là khách hàng hay không
        /// </summary>
        public bool IsCustomer { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [MaxLength(255, ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_MaxLength_Address))]
        public string? Address { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [MaxLength(50, ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_MaxLength_PhoneNumber))]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ website
        /// </summary>
        [MaxLength(255, ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_MaxLength_Website))]
        public string? Website { get; set; }

        /// <summary>
        /// Nhóm nhà cung cấp
        /// </summary>
        public List<Guid>? GroupIds { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [MaxLength(36, ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_MaxLength_EmployeeId))]
        public string? EmployeeId { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        [MaxLength(10000, ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_MaxLength_Note))]
        public string? Note { get; set; }

        /// <summary>
        /// Xưng hô
        /// </summary>
        public string? Vocative { get; set; }

        /// <summary>
        /// Tên người liên hệ
        /// </summary>
        [MaxLength(100, ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_MaxLength_NameContacter))]
        public string? NameContacter { get; set; }

        /// <summary>
        /// Email người liên hệ
        /// </summary>
        [MaxLength(100, ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_MaxLength_EmailContacter))]
        [ValidateEmail(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_Email_ErrorFormat))]
        public string? EmailContacter { get; set; }

        /// <summary>
        /// Điện thoại di động người liên hệ
        /// </summary>
        [MaxLength(50, ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_MaxLength_PhoneNumberContacter))]
        public string? PhoneNumberContacter { get; set; }

        /// <summary>
        /// Điện thoại cố định
        /// </summary>
        [MaxLength(50, ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_MaxLength_PhoneLandlineContacter))]
        public string? PhoneLandlineContacter { get; set; }

        /// <summary>
        /// Số chứng minh người liên hệ
        /// </summary>
        [MaxLength(25, ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_MaxLength_IdentityNumberContacter))]
        [ValidateIdentityNumber(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_IdentityNumber_Error))]
        public string? IdentityNumberContacter { get; set; }

        /// <summary>
        /// Ngày cấp chứng minh
        /// </summary>
        [ValidateDatetime("Validate_Invalid_IdentityDate")]
        public DateTime? IdentityDateContacter { get; set; }

        /// <summary>
        /// Nơi cấp chứng minh
        /// </summary>
        [MaxLength(255, ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_MaxLength_IdentityPlace))]
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Đại diện theo pháp luật
        /// </summary>
        [MaxLength(255, ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_MaxLength_Lawyer))]
        public string? Lawyer { get; set; }

        /// <summary>
        /// Tên người nhận
        /// </summary>
        [MaxLength(100, ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_MaxLength_NameReceiver))]
        public string? NameReceiver { get; set; }

        /// <summary>
        /// Email người nhận
        /// </summary>
        [MaxLength(100, ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_MaxLength_EmailReceiver))]
        [ValidateEmail(ErrorMessageResourceType = typeof(ResourceVN), ErrorMessageResourceName = nameof(ResourceVN.Validate_Email_ErrorFormat))]
        public string? EmailReceiver { get; set; }

        /// <summary>
        /// Điện thoại người nhận
        /// </summary>
        [MaxLength(50, ErrorMessageResourceType = typeof(ProviderVN), ErrorMessageResourceName = nameof(ProviderVN.Validate_MaxLength_PhoneNumberReceiver))]
        public string? PhoneNumberReceiver { get; set; }

        /// <summary>
        /// Điều khoản thanh toán
        /// </summary>
        public string? TermPaymentId { get; set; }

        /// <summary>
        /// Số ngày được nợ
        /// </summary>
        public long NumberDayOwed { get; set; }

        /// <summary>
        /// Số nợ phải trả
        /// </summary>
        public decimal? AmountDebt { get; set; }

        /// <summary>
        /// Tài khoản công nợ phả thu
        /// </summary>
        public string? AccountReceivableId { get; set; }

        /// <summary>
        /// Tài khoản công nợ phải trả
        /// </summary>
        public string? AccountPayableId { get; set; }

        /// <summary>
        /// Vị trị địa lí: Quốc gia
        /// </summary>
        public string? LocationCountry { get; set; }

        /// <summary>
        /// Vị trị địa lí: Tỉnh/Thành phố
        /// </summary>
        public string? LocationCity { get; set; }

        /// <summary>
        /// Vị trị địa lí: Quận/Huyện
        /// </summary>
        public string? LocationDistrict { get; set; }

        /// <summary>
        /// Vị trị địa lí: Xã/Phường
        /// </summary>
        public string? LocationVillage { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        public List<AccountProvider>? AccountProviders { get; set; }

        /// <summary>
        /// Địa chỉ giao hàng
        /// </summary>
        public List<DeliveryAddress>? DeliveryAddresses { get; set; }
    }
}
