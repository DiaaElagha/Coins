using Coins.Core.Constants.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Coins.Core.Models.DtoAPI.User
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public Gender? Gender { get; set; }
        public DateTimeOffset Birthdate { get; set; }
        public string Address { get; set; }

        public string FcmToken { get; set; }
        public bool IsAnonymous { get; set; }
        public string DeviceId { get; set; }
    }
}
