using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Models.DtoAPI.Store
{
    public class SetInvoiceDto
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public float InvoiceValue { get; set; }
        [Required]
        public string InvoiceNumber { get; set; }
    }
}
