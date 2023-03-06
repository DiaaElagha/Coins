using Coins.Core.Models.Domins.StoresInfo;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coins.Core.Models.DtoAPI.Store
{
    public class StorePriceTypeDto
    {
        public int StorePriceTypeId { get; set; }

        public string StorePriceTypeAr { get; set; }
        public string StorePriceTypeEn { get; set; }

    }
}
