using Coins.Core.Constants.Enums;
using Coins.Core.Models.Domins.StoresInfo;
using Coins.Entities.Domins.Auth;
using Coins.Entities.Domins.Base;
using Coins.Entities.Domins.StoresInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.Domins.Auth
{
    public class UserCoins : BaseEntity
    {
        public UserCoins()
        {
            CoinStartDate = DateTimeOffset.Now;
            NumberOfCoinCollected = 0;
            Remaining = 0;
        }

        [Key]
        public int UserCoinId { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser UserRelated { get; set; }

        public int StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]
        public Stores Stores { get; set; }

        public int? StoreBranchId { get; set; }
        [ForeignKey(nameof(StoreBranchId))]
        public StoreBranchs StoreBranch { get; set; }

        public SocialType? SocialType { get; set; }

        public int NumberOfCoinCollected { get; set; }
        public int Remaining { get; set; }
        public DateTimeOffset CoinStartDate { get; set; }
        public DateTimeOffset CoinExpiryDate { get; set; }
        public bool IsDetected { get; set; }

        public float? InvoiceValue { get; set; }
        public string InvoiceNumber { get; set; }

    }
}
