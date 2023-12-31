﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MISA.WebFresher042023.Core.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class AccountVN {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AccountVN() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MISA.WebFresher042023.Core.Resources.AccountVN", typeof(AccountVN).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tài khoản này có ràng buộc với các tài khoản khác, không được sửa.
        /// </summary>
        public static string ErrorLogic_Constraint_AccountNumber {
            get {
                return ResourceManager.GetString("ErrorLogic_Constraint_AccountNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Số tài khoản đã tồn tại trong hệ thống.
        /// </summary>
        public static string ErrorLogic_Exist_AccountNumber {
            get {
                return ResourceManager.GetString("ErrorLogic_Exist_AccountNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Số tài khoản có độ dài &gt; 3 phải điền thông tin &lt;tài khoản tổng hợp&gt;..
        /// </summary>
        public static string ErrorLogic_Fail_ParentId {
            get {
                return ResourceManager.GetString("ErrorLogic_Fail_ParentId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Không thể xóa danh mục cha nếu chưa xóa danh mục con..
        /// </summary>
        public static string ErrorLogic_NotDelete_Account_1 {
            get {
                return ResourceManager.GetString("ErrorLogic_NotDelete_Account_1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Không thể xóa tài khoản đã có phát sinh trên các danh mục, chứng từ..
        /// </summary>
        public static string ErrorLogic_NotDelete_Account_2 {
            get {
                return ResourceManager.GetString("ErrorLogic_NotDelete_Account_2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tài khoản cha đang ở trạng thái ngừng sử dụng. Bạn không thể thiết lập trạng thái sử dụng cho tài khoản con..
        /// </summary>
        public static string ErrorLogic_NotUsing_Account {
            get {
                return ResourceManager.GetString("ErrorLogic_NotUsing_Account", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tài khoản &lt;{0}&gt; đã có phát sinh trên các danh mục, chứng từ.Bạn không được phép sửa tài khoản này..
        /// </summary>
        public static string ErrorLogic_Update_Account {
            get {
                return ResourceManager.GetString("ErrorLogic_Update_Account", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Danh_sach_he_thong_tai_khoan.xlsx.
        /// </summary>
        public static string Export_FileName {
            get {
                return ResourceManager.GetString("Export_FileName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DANH SÁCH HỆ THỐNG TÀI KHOẢN.
        /// </summary>
        public static string Export_Title_Account {
            get {
                return ResourceManager.GetString("Export_Title_Account", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tên tài khoản.
        /// </summary>
        public static string Export_Title_AccountName {
            get {
                return ResourceManager.GetString("Export_Title_AccountName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tên tiếng anh.
        /// </summary>
        public static string Export_Title_AccountNameEnglish {
            get {
                return ResourceManager.GetString("Export_Title_AccountNameEnglish", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Số tài khoản.
        /// </summary>
        public static string Export_Title_AccountNumber {
            get {
                return ResourceManager.GetString("Export_Title_AccountNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Diễn giải.
        /// </summary>
        public static string Export_Title_Explain {
            get {
                return ResourceManager.GetString("Export_Title_Explain", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tính chất.
        /// </summary>
        public static string Export_Title_Nature {
            get {
                return ResourceManager.GetString("Export_Title_Nature", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Trạng thái.
        /// </summary>
        public static string Export_Title_State {
            get {
                return ResourceManager.GetString("Export_Title_State", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Số tài khoản không hợp lệ. Tài khoản chi tiết phải bắt đầu từ tài khoản tổng hợp..
        /// </summary>
        public static string InValid_ParentId {
            get {
                return ResourceManager.GetString("InValid_ParentId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Phải điền thông tin tài khoản tổng hợp.
        /// </summary>
        public static string Required_ParentId {
            get {
                return ResourceManager.GetString("Required_ParentId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tài khoản.
        /// </summary>
        public static string Text_Account {
            get {
                return ResourceManager.GetString("Text_Account", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Khách hàng.
        /// </summary>
        public static string Text_Customer {
            get {
                return ResourceManager.GetString("Text_Customer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nhân viên.
        /// </summary>
        public static string Text_Employee {
            get {
                return ResourceManager.GetString("Text_Employee", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nhà cung cấp.
        /// </summary>
        public static string Text_Provider {
            get {
                return ResourceManager.GetString("Text_Provider", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ngưng sử dụng.
        /// </summary>
        public static string Text_State_StopUsing {
            get {
                return ResourceManager.GetString("Text_State_StopUsing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Đang sử dụng.
        /// </summary>
        public static string Text_State_Using {
            get {
                return ResourceManager.GetString("Text_State_Using", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Số tài khoản không hợp lệ.Số tài khoản không hợp lệ. Số tài khoản có phần đầu &lt;{0}&gt; trùng với tài khoản &lt;{1}&gt; cùng cấp..
        /// </summary>
        public static string Validate_ExitStartWith_AccountNumber {
            get {
                return ResourceManager.GetString("Validate_ExitStartWith_AccountNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tên tiếng anh tối đa 255 kí tự.
        /// </summary>
        public static string Validate_MaxLength_AccountEnglishName {
            get {
                return ResourceManager.GetString("Validate_MaxLength_AccountEnglishName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tên tài khoản tối đa 255 kí tự.
        /// </summary>
        public static string Validate_MaxLength_AccountName {
            get {
                return ResourceManager.GetString("Validate_MaxLength_AccountName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Số tài khoản tối đa 20 kí tự.
        /// </summary>
        public static string Validate_MaxLength_AccountNumber {
            get {
                return ResourceManager.GetString("Validate_MaxLength_AccountNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tính chất tối đa 255 kí tự.
        /// </summary>
        public static string Validate_MaxLength_Nature {
            get {
                return ResourceManager.GetString("Validate_MaxLength_Nature", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tài khoản tổng hợp tối đa 36 kí tự.
        /// </summary>
        public static string Validate_MaxLength_ParentId {
            get {
                return ResourceManager.GetString("Validate_MaxLength_ParentId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Số tài khoản tối thiểu 3 kí tự.
        /// </summary>
        public static string Validate_MinLength_AccountNumber {
            get {
                return ResourceManager.GetString("Validate_MinLength_AccountNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tài khoản tổng hợp không tồn tại trong hệ thống.
        /// </summary>
        public static string Validate_NotExist_ParentId {
            get {
                return ResourceManager.GetString("Validate_NotExist_ParentId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tên tài khoản không được để trống.
        /// </summary>
        public static string Validate_NotNull_AccountName {
            get {
                return ResourceManager.GetString("Validate_NotNull_AccountName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Số tài khoản không được để trống.
        /// </summary>
        public static string Validate_NotNull_AccountNumber {
            get {
                return ResourceManager.GetString("Validate_NotNull_AccountNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tính chất không được để trống.
        /// </summary>
        public static string Validate_NotNull_Nature {
            get {
                return ResourceManager.GetString("Validate_NotNull_Nature", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to đang ở trạng thái &lt;Ngừng sử dụng&gt;. Bạn không được phép chọn thuộc tài khoản này..
        /// </summary>
        public static string Validate_UsingState_ParentId {
            get {
                return ResourceManager.GetString("Validate_UsingState_ParentId", resourceCulture);
            }
        }
    }
}
