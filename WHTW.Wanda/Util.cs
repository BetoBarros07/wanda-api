using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WHTW.Wanda
{
    public static class Util
    {
        public static string Encode(this string value)
        {
            value += "|2d331cca-f6c0-40c0-bb43-6e32989c2881";
            var md5 = MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(value));
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        public static Guid GetUserId()
        {
            return new Guid(((ClaimsIdentity)HttpContext.Current.User.Identity).Claims.FirstOrDefault(x => x.Type == "id").Value);
        }
    }
}