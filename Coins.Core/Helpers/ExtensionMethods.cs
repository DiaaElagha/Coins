using Coins.Core.Helpers.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using NetTopologySuite.Geometries;
using ProjNet.CoordinateSystems.Transformations;
using ProjNet.CoordinateSystems;
using GeoAPI.CoordinateSystems.Transformations;
using NetTopologySuite;

namespace Coins.Core.Helpers
{
    public static class ExtensionMethods
    {
        public static Point CreatePoint(double latitude, double longitude)
        {
            CoordinateTransformationFactory ctfac = new CoordinateTransformationFactory();
            var from = GeographicCoordinateSystem.WGS84;
            var to = ProjectedCoordinateSystem.WGS84_UTM(30, true);
            // convert points from one coordinate system to another
            ICoordinateTransformation trans = ctfac.CreateFromCoordinateSystems(from, to);
            var coordinate = new GeoAPI.Geometries.Coordinate(longitude, latitude);
            var mathTransform = trans.MathTransform;
            var location = mathTransform.Transform(coordinate);
            return new Point(location.X, location.Y) { SRID = 4326, };
        }

        public static string GetDate(DateTime date)
        {
            TimeSpan span = (DateTime.Now - date);
            var days = span.Days;
            var hours = span.Hours;
            var minutes = span.Minutes;
            var seconds = span.Seconds;
            if (minutes == 0 && days == 0 && hours == 0)
            {
                return String.Format("{0} seconds", seconds);
            }
            if (days == 0 && hours == 0)
            {
                return String.Format("{0} minutes", minutes);
            }
            if (days == 0 && hours != 0)
            {
                return String.Format("{0} hours, {1} minutes", hours, minutes);
            }
            if (days != 0)
            {
                return String.Format("{0} days, {1} hours", days, hours);
            }
            return String.Format("{0} days, {1} hours, {2} minutes", days, hours, minutes);
        }

        public static string AutoIncrement(string lastSerialNum)
        {
            int id = Convert.ToInt32(lastSerialNum);
            id = id + 1;
            string autoId = String.Format("{0:00000000}", id);
            return autoId;
        }

        public static string GetDescription(this Enum e)
        {
            var attribute =
                e.GetType()
                    .GetTypeInfo()
                    .GetMember(e.ToString())
                    .FirstOrDefault(member => member.MemberType == MemberTypes.Field)
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .SingleOrDefault()
                    as DescriptionAttribute;
            return attribute?.Description ?? e.ToString();
        }

        public static List<DataPage> Pagenate(int totalItems, out string showing, out bool nextEnabled, out bool prevEnabled, out int totalPages, int currentPage = 1, int pageSize = 10, int maxPages = 5)
        {
            var pager = new Pager(totalItems, currentPage, pageSize, maxPages);
            totalPages = pager.TotalPages;
            var pages = new List<DataPage>();
            foreach (var item in pager.Pages)
            {
                DataPage page;
                if (item == currentPage)
                {
                    page = new DataPage
                    {
                        PageNumber = item,
                        IsSelected = true,
                    };
                }
                else
                    page = new DataPage
                    {
                        PageNumber = item,
                        IsSelected = false,
                    };
                pages.Add(page);
            }

            var start = (pageSize * (currentPage - 1)) + 1;
            if (totalPages == 1)
            {
                showing = $"Showing {start} to {totalItems} of {totalItems}) entries";
            }
            else
            {
                var to = totalItems == 0 ? 0 : currentPage == totalPages ? (totalItems % (totalPages - 1) + start - 1) : start + pageSize - 1;
                showing = $"Showing {start} to {to} of {totalItems}) entries";
            }
            if (currentPage == 1)
                prevEnabled = false;
            else
                prevEnabled = true;

            if (currentPage >= totalPages)
                nextEnabled = false;
            else
                nextEnabled = true;


            return pages;
        }

        public static string GenerateQR(string data)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            var stream = new MemoryStream();
            qrCode.GetGraphic(20).Save(stream, ImageFormat.Png);
            var bytes = stream.ToArray();
            return Convert.ToBase64String(bytes);
        }

        public static string GetRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", "");
            return path.Substring(0, 8);
        }

        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static Dictionary<int, string> GetEnumAsDictionary<T>()
        {
            var resultDictionary = new Dictionary<int, string>();
            foreach (var name in Enum.GetNames(typeof(T)))
            {
                resultDictionary.Add((int)Enum.Parse(typeof(T), name), name);
            }
            return resultDictionary;
        }

        public static List<string> GetEnumAsList<T>()
        {
            List<string> listTypes = typeof(T)
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly)
                .Where(fi => fi.FieldType == typeof(string))
                .Select(fi => fi.Name)
                .ToList();
            return listTypes;
        }

        public static List<T> GetEnumAsListValues<T>()
        {
            List<T> listTypes = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            return listTypes;
        }

        public static string ByteToString(this byte[] arr)
        {
            string fileString = Convert.ToBase64String(arr);
            return fileString;
        }

    }

}
