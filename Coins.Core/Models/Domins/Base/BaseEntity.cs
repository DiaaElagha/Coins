using Coins.Entities.Domins.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Coins.Entities.Domins.Base
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreateAt = DateTimeOffset.Now;
            IsDeleted = false;
        }
        public DateTimeOffset? CreateAt { get; set; }

        public Guid? CreateByUserId { get; set; }
        [ForeignKey(nameof(CreateByUserId))]
        public ApplicationUser ApplicationUserCreate { get; set; }

        public Guid? UpdateByUserId { get; set; }
        [ForeignKey(nameof(UpdateByUserId))]
        public ApplicationUser ApplicationUserUpdate { get; set; }

        public DateTimeOffset? UpdateAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
