using Coins.Core.Models.Domins.StoresInfo;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coins.Core.Models.DtoAPI.Store
{
    public class StoreCategoryDto
    {
        public int StoreCategoryId { get; set; }

        public string StoreCategoryNameAr { get; set; }
        public string StoreCategoryNameEn { get; set; }
        public string StoreCategoryAttachmentId { get; set; }

    }
}
