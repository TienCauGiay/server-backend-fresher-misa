using MISA.WebFresher042023.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher042023.Core.DTO.Providers
{
    public class ProviderDto
    {
        /// <summary>
        /// ID của nhà cung cấp
        /// </summary>
        public Guid ProviderId { get; set; }

        /// <summary>
        /// Mã nhà cung cấp
        /// </summary>
        public string ProviderCode { get; set; }

        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// Mã số thuế của nhà cung cấp
        /// </summary>
        public string? TaxCode { get; set; }

        /// <summary>
        /// Nhà cung cấp có phải là cá nhân hay không
        /// </summary>
        public bool IsPersonal { get; set; }

        /// <summary>
        /// Nhà cung cấp có phải là khách hàng hay không
        /// </summary>
        public bool IsCustomer { get; set; }

        /// <summary>
        /// Địa chỉ của nhà cung cấp
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Số điện thoại của nhà cung cấp
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Địa chỉ website của nhà cung cấp
        /// </summary>
        public string? Website { get; set; }

        /// <summary>
        /// Thông tin nhóm nhà cung cấp
        /// </summary>
        public object? GroupProvider { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string? EmployeeId { get; set; }

        /// <summary>
        /// Họ tên nhân viên
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// Thông tin liên hệ
        /// </summary>
        public string? InfoContact { get; set; }

        /// <summary>
        /// Điều khoản thanh toán
        /// </summary>
        public string? TermPayment { get; set; }

        /// <summary>
        /// Địa chỉ khác
        /// </summary>
        public string? AddressOther { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? Note { get; set; }

        /// <summary>
        /// Xưng hô
        /// </summary>
        public string? Vocative { get; set; }

        /// <summary>
        /// Tên người liên hệ
        /// </summary>
        public string? NameContacter { get; set; }

        /// <summary>
        /// Địa chỉ email người liên hệ
        /// </summary>
        public string? EmailContacter { get; set; }

        /// <summary>
        /// Số điện thoại người liên hệ
        /// </summary>
        public string? PhoneNumberContacter { get; set; }

        /// <summary>
        /// Điện thoai cố định người liên hệ
        /// </summary>
        public string? PhoneLandlineContacter { get; set; }

        /// <summary>
        /// Số chứng minh nhân dân
        /// </summary>
        public string? IdentityNumberContacter { get; set; }

        /// <summary>
        /// Ngày cấp chứng minh
        /// </summary>
        public DateTime? IdentityDateContacter { get; set; }

        /// <summary>
        /// Nơi cấp chứng minh
        /// </summary>
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Đại diện theo pháp luật
        /// </summary>
        public string? Lawyer { get; set; }

        /// <summary>
        /// Tên người nhận
        /// </summary>
        public string? NameReceiver { get; set; }

        /// <summary>
        /// Email người nhận
        /// </summary>
        public string? EmailReceiver { get; set; }

        /// <summary>
        /// Điện thoại người nhận
        /// </summary>
        public string? PhoneNumberReceiver { get; set; }

        /// <summary>
        /// Điều khoản thanh toán (id)
        /// </summary>
        public string? TermPaymentId { get; set; }

        /// <summary>
        /// Tên điều khoản thanh toán
        /// </summary>
        public string? TermPaymentName { get; set; }

        /// <summary>
        /// Số ngày được nợ
        /// </summary>
        public long NumberDayOwed { get; set; }

        /// <summary>
        /// Số nợ phải trả
        /// </summary>
        public decimal? AmountDebt { get; set; }

        /// <summary>
        /// Id tài khoản công nợ phải thu
        /// </summary>
        public string? AccountReceivableId { get; set; }

        /// <summary>
        /// Tên tài khoản công nợ phải thu
        /// </summary>
        public string? AccountReceivableNumber { get; set; }

        /// <summary>
        /// Id tài khoản công nợ phải trả
        /// </summary>
        public string? AccountPayableId { get; set; }

        /// <summary>
        /// Tên tài khoản công nợ phải trả
        /// </summary>
        public string? AccountPayableNumber { get; set; }
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
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
    }
}
