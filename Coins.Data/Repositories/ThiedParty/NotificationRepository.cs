using Coins.Core.Helpers;
using Coins.Core.Models.DtoAPI.Notification;
using Coins.Core.Services;
using Coins.Core.Services.ThiedParty;
using Coins.Core.Settings;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Data.Repositories.ThiedParty
{
    public class NotificationRepository : INotificationsService
    {
        FirebaseApp app;
        FirebaseMessaging fcm;
        public NotificationRepository()
        {
            InitialFirebaseService();
        }

        void InitialFirebaseService()
        {
            var authjson = new MemoryStream(Properties.Resources.AuthFirebase);
            try
            {
                app = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromStream(authjson)
                }, FirebaseSettings.App);
            }
            catch (Exception ex)
            {
                ex.Log();
                app = FirebaseApp.GetInstance(FirebaseSettings.App);
            }
            if (app == null)
                return;
            fcm = FirebaseMessaging.GetMessaging(app);
        }

        public async Task Send(string id, NotificationDto notifiObject, Dictionary<string, string> AdditionalData = null)
        {
            if (fcm == null)
                return;
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = notifiObject.TitleAr,
                    Body = notifiObject.BodyAr
                },
                Data = AdditionalData,
                Token = id
            };
            await fcm?.SendAsync(message);
        }

        public async Task Send(IEnumerable<string> ids, NotificationDto notifiObject, Dictionary<string, string> AdditionalData = null)
        {
            var message = new MulticastMessage()
            {
                Notification = new Notification
                {
                    Title = notifiObject.TitleAr,
                    Body = notifiObject.BodyAr
                },
                Data = AdditionalData,
                Tokens = ids.ToArray()
            };
            await fcm?.SendMulticastAsync(message);
        }

        public async Task SendToAll(NotificationDto notifiObject, Dictionary<string, string> AdditionalData = null)
        {
            var message = new Message()
            {
                Notification = new Notification
                {
                    Title = notifiObject.TitleAr,
                    Body = notifiObject.BodyAr
                },
                Data = AdditionalData,
                Topic = "All"
            };
            await fcm?.SendAsync(message);
        }
    }
}
