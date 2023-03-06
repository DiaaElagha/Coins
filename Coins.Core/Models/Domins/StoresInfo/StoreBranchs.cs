using Coins.Core.Models.Domins.Auth;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.Auth;
using Coins.Entities.Domins.Base;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Entities.Domins.StoresInfo
{
    public class StoreBranchs : BaseEntity
    {
        public StoreBranchs()
        {
            StoresList = new HashSet<Stores>();
            StoresAttachmentsList = new HashSet<StoresBranchsAttachments>();
            StoreBranchsAdvantagesList = new HashSet<StoreBranchsAdvantages>();
            UserCoinsList = new HashSet<UserCoins>();
            UserStoresList = new HashSet<UserStores>();
            StoreRateList = new HashSet<StoreRate>();
            StoreUserFavoritesList = new HashSet<StoreUserFavorites>();
            UserSocialStoreList = new HashSet<UserSocialStore>();
            UsersList = new HashSet<ApplicationUser>();

            IsMainBranch = false;
            NumOfSearch = 0;
            AvgRate = 0;
        }

        [Key]
        public int BranchId { get; set; }

        public string BranchNameAr { get; set; }
        public string BranchNameEn { get; set; }
        public string BranchDescriptionAr { get; set; }
        public string BranchDescriptionEn { get; set; }
        public int StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]
        public Stores Store { get; set; }

        public double AvgRate { get; set; }
        public int NumOfSearch { get; set; }
        public bool IsMainBranch { get; set; }

        [NotMapped]
        public string ReferrralCode { get; set; }
        public string BranchMainAttachmentId { get; set; }

        public double? BranchLatitudeLocation { get; set; }
        public double? BranchLongitudeLocation { get; set; }

        [Column(TypeName = "geometry")]
        public Point Location { get; set; }

        public virtual ICollection<Stores> StoresList { get; set; }
        public virtual ICollection<StoresBranchsAttachments> StoresAttachmentsList { get; set; }
        public virtual ICollection<StoreBranchsAdvantages> StoreBranchsAdvantagesList { get; set; }
        public virtual ICollection<UserCoins> UserCoinsList { get; set; }
        public virtual ICollection<UserStores> UserStoresList { get; set; }
        public virtual ICollection<StoreRate> StoreRateList { get; set; }
        public virtual ICollection<StoreUserFavorites> StoreUserFavoritesList { get; set; }
        public virtual ICollection<UserSocialStore> UserSocialStoreList { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<ApplicationUser> UsersList { get; set; }
    }
}
