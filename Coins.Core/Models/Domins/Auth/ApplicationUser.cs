using Coins.Core.Constants.Enums;
using Coins.Core.Models.Domins.Auth;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.Notification;
using Coins.Entities.Domins.StoresInfo;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Entities.Domins.Auth
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            CreateAt = DateTimeOffset.Now;
            IsActive = true;
            IsAnonymous = true;
            IsVerify = false;

            NotificationsListRecever = new HashSet<Notifications>();
            NotificationsListSender = new HashSet<Notifications>();
            MessageSMSReceverList = new HashSet<MessageSMS>();
            UserCoinsList = new HashSet<UserCoins>();
            UserStoresList = new HashSet<UserStores>();
            StoreRateList = new HashSet<StoreRate>();
        }

        public string FullName { get; set; }

        public Gender? Gender { get; set; }

        public DateTimeOffset? Birthdate { get; set; }
        
        public string Address { get; set; }
        public DateTimeOffset? CreateAt { get; set; }
        public DateTimeOffset? UpdateAt { get; set; }

        public int? StoreBranchId { get; set; }
        [ForeignKey(nameof(StoreBranchId))]
        public virtual StoreBranchs StoreBranch { get; set; }

        public int? StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]
        public virtual Stores Store { get; set; }

        public double? LastUserLatitudeLocation { get; set; }
        public double? LastUserLongitudeLocation { get; set; }

        public bool IsActive { get; set; }
        [Required]
        public string Role { get; set; }
        public DateTimeOffset LastLogin { get; set; }
        public string FacebookId { get; set; }
        public string DeviceId { get; set; }
        public byte[] CodeHash { get; set; }
        public byte[] CodeSalt { get; set; }
        public string FcmToken { get; set; }
        public string AccessToken { get; set; }
        public int? ForgetPasswordCode { get; set; }
        public bool IsAnonymous { get; set; }
        public bool IsVerify { get; set; }
        public DateTimeOffset? ExpiryDate { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Notifications> NotificationsListRecever { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<Notifications> NotificationsListSender { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<MessageSMS> MessageSMSReceverList { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<UserCoins> UserCoinsList { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<UserStores> UserStoresList { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<StoreRate> StoreRateList { get; set; }

    }
}
