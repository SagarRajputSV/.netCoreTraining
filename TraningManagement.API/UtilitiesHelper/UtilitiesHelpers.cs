using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using TrainingManagement.Application.Models;

namespace TrainingManagement.API.UtilitiesHelper
{
    public class UtilitiesHelpers:IUtilitiesHelpers
    {
        private readonly ClaimsPrincipal _claimsPrincipal;

        public UtilitiesHelpers(ClaimsPrincipal claimsPrincipal)
        {
            _claimsPrincipal = claimsPrincipal;
        }

        public ClaimModel SetClaims()
        {
            if (_claimsPrincipal?.Claims == null) return null;
            var claims = from c in _claimsPrincipal.Claims select new { c.Type, c.Value };
            var enumerable = claims.ToList();
            if (!enumerable.Any()) return null;
            var claimModel = new ClaimModel
            {
                RoleName = enumerable.FirstOrDefault(x => x.Type == "role")?.Value,
                UserName = enumerable.FirstOrDefault(x => x.Type == "name")?.Value,
                Email = enumerable.FirstOrDefault(x => x.Type == "email")?.Value,
                UserId = enumerable.FirstOrDefault(x => x.Type == "sub")?.Value,
                RoleId = Convert.ToInt16(enumerable.FirstOrDefault(x => x.Type == "roleId")?.Value),
            };
            return claimModel;

        }

        public static ClaimModel SetClaims(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal?.Claims == null) return null;
            var claims = from c in claimsPrincipal.Claims select new { c.Type, c.Value };
            var enumerable = claims.ToList();
            if (!enumerable.Any()) return null;
            var claimModel = new ClaimModel
            {
                RoleName = enumerable.FirstOrDefault(x => x.Type == "role")?.Value,
                UserName = enumerable.FirstOrDefault(x => x.Type == "UserName")?.Value,
                Email = enumerable.FirstOrDefault(x => x.Type == "Email")?.Value,
                UserId = enumerable.FirstOrDefault(x => x.Type == "UserId")?.Value,
                RoleId = Convert.ToInt16(enumerable.FirstOrDefault(x => x.Type == "roleId")?.Value)
            };
            return claimModel;
        }

        //public static void CheckClaim(ClaimModel claimModel)
        //{
        //    ThrowException.IfNullOrWhiteSpace(claimModel?.RoleName);
        //    ThrowException.IfNullOrWhiteSpace(claimModel?.Email);
        //    ThrowException.IfNullOrWhiteSpace(claimModel?.UserName);
        //}

        public static string Base64ForUrlEncode(string str)
        {
            var encbuff = Encoding.UTF8.GetBytes(str);
            return HttpUtility.UrlEncode(encbuff);
        }

        public static string Base64ForUrlDecode(string str)
        {
            var decbuff = HttpUtility.UrlDecode(str);
            return decbuff != null ? Encoding.UTF8.GetString(Encoding.ASCII.GetBytes(decbuff)) : null;
        }

        public static string GetUrlWithProtocols(string url, bool isHttp)
        {
            //CurrentHttpProtocol
            if (isHttp)
            {
                if (!url.Contains("http://") && !url.Contains("https://"))
                {
                    url = "https://" + url;
                }
            }
            
            return url;
        }
    }
}
