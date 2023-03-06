using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coins.Web.Helper
{
    public static class ShowMessage
    {
        public static string AddSuccessResult(string msg = null)
        {
            return JsonConvert.SerializeObject(
                new
                {
                    status = 1,
                    msg = String.IsNullOrEmpty(msg) ? "s: تمت الاضافة بنجاح" : msg,
                    close = 1
                });
        }
        public static string SendSuccessResult()
        {
            return JsonConvert.SerializeObject(new { status = 1, msg = "s: تم الارسال بنجاح", close = 1 });
        }

        public static string EditSuccessResult()
        {
            return JsonConvert.SerializeObject(new { status = 1, msg = "s: تم التعديل بنجاح", close = 1 });
        }

        public static string DeleteSuccessResult()
        {
            return JsonConvert.SerializeObject(new { status = 1, msg = "s: تمت الحذف بنجاح", close = 1 });
        }

        public static string FailedResult()
        {
            return JsonConvert.SerializeObject(new { status = 1, msg = "e: فشلت العملية", close = 2 });
        }

        public static string DuplicationResult()
        {
            return JsonConvert.SerializeObject(new { status = 1, msg = "e: العنصر موجود مسبقا", close = 2 });
        }

    }

    public class MessegeResult
    {
        public string msg { get; set; }
        public string status { get; set; }
        public string id { get; set; }
    }

}