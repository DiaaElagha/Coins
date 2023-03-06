using System;
using System.ComponentModel.DataAnnotations;

namespace Coins.Core.Models.DtoAPI.Auth
{
    public class LoginDto
    {
        [Required]
        public string DeviceId { get; set; }
        [Required]
        public string MobileNo { get; set; }
    }
}
