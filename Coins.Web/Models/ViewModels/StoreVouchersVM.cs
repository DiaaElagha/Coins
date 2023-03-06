using Coins.Core.Constants.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Models.ViewModels
{
    public class StoreVouchersVM
    {
        public StoreVouchersVM()
        {
            VoucherFirstTime = new VoucherFirstTime(VoucherType.FirstTime);
        }

        [Display(Name = "النقاط المستبدلة مقابل كل دينار خصم")]
        //[Required(ErrorMessage = "يرجى ادخال النقاط")]
        [Range(0, 1000000, ErrorMessage = "يرجى ادخال قيمة صحيحة")]
        public int NumberRedeemedCoinsForEveryCurrency { get; set; }

        [Display(Name = "قم بتعيين الحد الأدنى من النقاط المطلوبة لاسترداد هذه المافأة")]
        [Required(ErrorMessage = "يرجى ادخال النقاط")]
        [Range(0, 1000000, ErrorMessage = "يرجى ادخال قيمة صحيحة")]
        public int DefineMinCoinsRedeemVoucherDiscount { get; set; }
        public bool IsActiveMinCoinsRedeemVoucherDiscount { get; set; }

        [Display(Name = " حدد الحد الأقصى من النقاط التى يمكن للعميل إنفاقها على هذه المافأة")]
        [Required(ErrorMessage = "يرجى ادخال النقاط")]
        [Range(0, 1000000, ErrorMessage = "يرجى ادخال قيمة صحيحة")]
        public int DefineMaxCoinsToSpentVoucherDiscount { get; set; }
        public bool IsActiveMaxCoinsToSpentVoucherDiscount { get; set; }

        public int? VoucherDiscountExpiredAfterDay { get; set; }
        public bool IsActiveVoucherDiscountExpiredAfterDay { get; set; }

        public bool IsActiveFirstTimeVoucher { get; set; }

        public bool IsActiveVoucherDiscount { get; set; }

        public VoucherFirstTime VoucherFirstTime { get; set; }
    }

    public class VoucherFirstTime : BaseVoucherVM
    {
        public VoucherFirstTime(VoucherType voucherType) : base(voucherType) { }
    }

    public class VoucherFriendReward : BaseVoucherVM
    {
        public VoucherFriendReward(VoucherType voucherType) : base(voucherType) { }

        [Display(Name = "قيمة الخصم")]
        [Required(ErrorMessage = "يرجى ادخال القيمة")]
        [Range(0, 1000000, ErrorMessage = "يرجى ادخال قيمة صحيحة")]
        public float VoucherDiscountValue { get; set; }
    }

    public class BaseVoucherVM
    {
        public BaseVoucherVM(VoucherType voucherType)
        {
            VoucherType = voucherType;
        }
        [Display(Name = "عنوان القسيمة بالعربي")]
        //[Required(ErrorMessage = "يرجى ادخال العنوان")]
        public string VoucherNameAr { get; set; }
        [Display(Name = "عنوان القسيمة بالانجليزي")]
        //[Required(ErrorMessage = "يرجى ادخال العنوان")]
        public string VoucherNameEn { get; set; }
        [Display(Name = "وصف القسيمة بالعربي")]
        //[Required(ErrorMessage = "يرجى ادخال الوصف")]
        public string VoucherDescrptionAr { get; set; }
        [Display(Name = "وصف القسيمة بالانجليزي")]
        //[Required(ErrorMessage = "يرجى ادخال الوصف")]
        public string VoucherDescrptionEn { get; set; }
        [Display(Name = "الشروط والأحكام")]
        //[Required(ErrorMessage = "يرجى ادخال الشروط والأحكام")]
        public string VoucherTerms { get; set; }
        [Display(Name = "تحميل صورة للقسيمة")]
        //[Required(ErrorMessage = "يرجى تحميل صورة للقسيمة")]
        public IFormFile VoucherMainAttachmentId { get; set; }

        public VoucherType VoucherType { get; set; }
    }


}
