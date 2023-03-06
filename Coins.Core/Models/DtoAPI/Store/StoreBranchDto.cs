using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.StoresInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Coins.Core.Models.DtoAPI.Store
{
    public class StoreBranchDto
    {
        public int BranchId { get; set; }
        public string BranchNameAr { get; set; }
        public string BranchNameEn { get; set; }
        public string BranchDescriptionAr { get; set; }
        public string BranchDescriptionEn { get; set; }
        public int StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]
        public StoresDto Store { get; set; }

        public int NumOfSearch { get; set; }
        public string ReferrralCode { get; set; }
        public bool IsMainBranch { get; set; }

        public string BranchMainAttachmentId { get; set; }

        public double? BranchLatitudeLocation { get; set; }
        public double? BranchLongitudeLocation { get; set; }

        public virtual ICollection<StoresBranchsAttachments> StoresAttachmentsList { get; set; }
        public virtual ICollection<StoreBranchsAdvantages> StoreBranchsAdvantagesList { get; set; }


        public int? Distance { get; set; }

    }
}
