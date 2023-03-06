using Coins.Core.Constants.Enums;
using Coins.Core.Models.DtoAPI.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins.Core.Constants
{
    public class NotificationsConst
    {
        public const string SoundDefault = "default";
        public const string DefaultLanguage = ArabicLanguage;
        public const string ArabicLanguage = "ar";
        public const string EnglishLanguage = "en";

        public static NotificationDto ReceivedCoins(string numCoins, string storeName)
        {
            var notification = GetNotification(NotificationReceivedCoins, NotificationType.Action, numCoins, storeName);
            return notification;
        }
        public static NotificationDto AlreadyClaimedReferralStore()
        {
            var notification = GetNotification(NotificationAlreadyClaimedReferralStore, NotificationType.Action);
            return notification;
        }
        public static NotificationDto VisitedStoreBefore()
        {
            var notification = GetNotification(NotificationVisitedStoreBefore, NotificationType.Action);
            return notification;
        }
        public static NotificationDto NotVisitedStoreBefore(string numCoins)
        {
            var notification = GetNotification(NotificationNotVisitedStoreBefore, NotificationType.Action, numCoins);
            return notification;
        }
        public static NotificationDto SocialMediaIntegration(string numCoins)
        {
            var notification = GetNotification(NotificationSocialMediaIntegration, NotificationType.Action, numCoins);
            return notification;
        }
        public static NotificationDto SuccessUnlockVoucher()
        {
            var notification = GetNotification(NotificationSuccessUnlockVoucher, NotificationType.Action);
            return notification;
        }
        public static NotificationDto FailedUnlockVoucher()
        {
            var notification = GetNotification(NotificationFailedUnlockVoucher, NotificationType.Action);
            return notification;
        }
        public static NotificationDto FailedRedeemedVoucher()
        {
            var notification = GetNotification(NotificationFailedRedeemedVoucher, NotificationType.Action);
            return notification;
        }
        public static NotificationDto SuccessRedeemedVoucher()
        {
            var notification = GetNotification(NotificationSuccessRedeemedVoucher, NotificationType.Action);
            return notification;
        }

        /// <summary> params
        /// 0: number of coins
        /// 1: store name
        /// </summary>
        #region Notifications
        public static Dictionary<string, string> NotificationReceivedCoins = new Dictionary<string, string>
        {
            ["ar-body"] = "لقد تلقيت {0} العملات من ({1}).",
            ["en-body"] = "You have received {0} Coins from ({1}).",
            ["ar-title"] = "تلقي عملات جديدة",
            ["en-title"] = "Receive new coins",
        };
        public static Dictionary<string, string> NotificationAlreadyClaimedReferralStore = new Dictionary<string, string>
        {
            ["ar-body"] = "عذرًا ، لقد طالبت بالفعل بنقاط الإحالة الخاصة بك في هذا المتجر",
            ["en-body"] = "Sorry, you have already claimed your referral points in this store.",
            ["ar-title"] = "نقاط الاحالة",
            ["en-title"] = "Referral points",
        };
        public static Dictionary<string, string> NotificationVisitedStoreBefore = new Dictionary<string, string>
        {
            ["ar-body"] = "عذرا ، لقد زرت هذا المتجر من قبل.",
            ["en-body"] = "Sorry, you have visited this store before.",
            ["ar-title"] = "زيارة المتجر",
            ["en-title"] = "Visiting store",
        };
        public static Dictionary<string, string> NotificationNotVisitedStoreBefore = new Dictionary<string, string>
        {
            ["ar-body"] = "لا يمكننا الانتظار لرؤيتك هنا. ستحصل على {0} نقطة إضافية في زيارتك الأولى.",
            ["en-body"] = "We cannot wait to see you here. You will get extra {0} points on your first visit.",
            ["ar-title"] = "زيارة المتجر",
            ["en-title"] = "Visiting store",
        };
        public static Dictionary<string, string> NotificationSocialMediaIntegration = new Dictionary<string, string>
        {
            ["ar-body"] = "شكرًا لك ، لقد أرسلنا لك {0} عملات معدنية كتقدير صغير منا.",
            ["en-body"] = "Thank you, we have sent you {0} coins as a small appreciation from us.",
            ["ar-title"] = "جائزة ربط السوشيال ميديا",
            ["en-title"] = "Social Media Award",
        };
        public static Dictionary<string, string> NotificationSuccessUnlockVoucher = new Dictionary<string, string>
        {
            ["ar-body"] = "رائع ، لقد فتحت هذه القسيمة ، يرجى الذهاب إلى المتجر واسترداد الهدية قبل انتهاء صلاحيتها.",
            ["en-body"] = "Yay, You have unlocked this voucher, please go to the store and redeem the gift before it expires.",
            ["ar-title"] = "فتح قسيمة جديدة",
            ["en-title"] = "Unlocked voucher",
        };
        public static Dictionary<string, string> NotificationFailedUnlockVoucher = new Dictionary<string, string>
        {
            ["ar-body"] = "عذرًا ، ليس لديك عملات كافية لفتح القسيمة.",
            ["en-body"] = "Sorry, you have insufficient coins to unlock this voucher.",
            ["ar-title"] = "لا يمكن فتح القسيمة",
            ["en-title"] = "Failed unlocked voucher",
        };
        public static Dictionary<string, string> NotificationFailedRedeemedVoucher = new Dictionary<string, string>
        {
            ["ar-body"] = "عذرا ، لقد انتهت صلاحية هذه القسيمة",
            ["en-body"] = "Sorry, This voucher has been expired.",
            ["ar-title"] = "قسيمة منتهية الصلاحية",
            ["en-title"] = "Expired voucher",
        };
        public static Dictionary<string, string> NotificationSuccessRedeemedVoucher = new Dictionary<string, string>
        {
            ["ar-body"] = "لقد استردت هذه القسيمة بنجاح.",
            ["en-body"] = "You have redeemed this voucher successfully.",
            ["ar-title"] = "تم استرداد القسيمة",
            ["en-title"] = "Redeemed voucher success",
        };

        #endregion


        #region Notification Utility
        static NotificationDto GetNotification(Dictionary<string, string> notificationDictionary, NotificationType type, params string[] parameter)
        {
            var notification = new NotificationDto
            {
                TitleAr = GetTitleOrDefault(ArabicLanguage, notificationDictionary),
                TitleEn = GetTitleOrDefault(EnglishLanguage, notificationDictionary),
                BodyAr = GetBodyOrDefault(ArabicLanguage, notificationDictionary, parameter),
                BodyEn = GetBodyOrDefault(EnglishLanguage, notificationDictionary, parameter),
                NotificationType = type
            };
            return notification;
        }

        public static string GetBodyOrDefault(string lang, Dictionary<string, string> dictionary, params string[] args)
        {
            if (dictionary.TryGetValue($"{lang}-body", out var value))
            {
                if (args != null && args.Length > 0)
                    value = string.Format(value, args);
                return value;
            }
            else if (dictionary.TryGetValue($"{DefaultLanguage}-body", out var defaultValue))
            {
                if (args != null && args.Length > 0)
                    defaultValue = string.Format(defaultValue, args);
                return defaultValue;
            }
            else
                return null;
        }

        public static string GetTitleOrDefault(string lang, Dictionary<string, string> dictionary, params string[] args)
        {
            if (dictionary.TryGetValue($"{lang}-title", out var value))
            {
                if (args != null && args.Length > 0)
                    value = string.Format(value, args);
                return value;
            }
            else if (dictionary.TryGetValue($"{DefaultLanguage}-title", out var defaultValue))
            {
                if (args != null && args.Length > 0)
                    defaultValue = string.Format(defaultValue, args);
                return defaultValue;
            }
            else
                return null;
        }
        #endregion


    }


}
