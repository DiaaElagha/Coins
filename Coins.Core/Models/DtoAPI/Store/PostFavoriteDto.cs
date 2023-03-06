using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.DtoAPI.Store
{
    public class PostFavoriteDto
    {
        [Required]
        public int StoreBranchId { get; set; }
        [Required]
        public bool Value { get; set; }
    }
}
