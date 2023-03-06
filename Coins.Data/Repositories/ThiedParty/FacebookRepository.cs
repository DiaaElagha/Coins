using Coins.Core.Constants.Enums;
using Coins.Core.Helpers;
using Coins.Core.Helpers.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Data.Repositories.ThiedParty
{
    public class FacebookRepository
    {
        private readonly HttpClient _httpClient;

        public FacebookRepository()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://graph.facebook.com/v9.0/")
            };
            _httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }


        public async Task<FacebookUserResource> GetUserFromFacebookAsync(string userId, string facebookToken)
        {
            var user = new { };

            var result = await GetAsync<dynamic>(facebookToken, "me", "fields=first_name,last_name,gender,birthday,picture.width(100).height(100),friends{id}");
            if (result == null)
            {
                throw new Exception("User from this token not exist");
            }

            var friends = new List<string>((result.friends.data as JArray).Select(x => x.ToString()));
            var account = new FacebookUserResource()
            {
                Id = result.id,
                FirstName = result.first_name,
                LastName = result.last_name,
                Picture = result.picture.data.url,
                Gender = result.gender == "male" ? Gender.Male : Gender.Female,
                Birthday = ((string)result.birthday).ParseDate("MM/dd/yyyy"),
                Friends = friends,
            };

            //var facebookFriends = _context.UsersRepo.Find(x => friesnd.Contains(x.FacebookId)).Select(x => x.ID).ToList();
            //user.FullName = $"{account.FirstName} {account.LastName}";
            //user.Avatar = account.Picture; // ToDo: Store Image and set the avatar id
            //user.FacebookId = account.Id;
            //user.FacebookFriends = facebookFriends;
            ////user.Gender = account.Gender;
            //user.Birthdate = account.Birthday;

            //_context.UsersRepo.Update(user);

            return account;
        }

        private async Task<T> GetAsync<T>(string accessToken, string endpoint, string args = null)
        {
            var response = await _httpClient.GetAsync($"{endpoint}?access_token={accessToken}&{args}");
            if (!response.IsSuccessStatusCode)
                return default(T);

            var result = await response.Content.ReadAsStringAsync();

            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(result);
        }
    }
}
