using Coins.Core.Models.DtoAPI.User;
using System;

namespace Coins.Core.Models.DtoAPI.Auth
{
    public class LoginResponseDto
    {
        public UserProfileDto ProfileData { get; set; }
        public string AccessToken { get; set; }
    }
}
