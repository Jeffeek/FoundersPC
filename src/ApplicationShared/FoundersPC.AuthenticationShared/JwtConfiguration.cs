#region Using namespaces

using System.Text;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace FoundersPC.AuthenticationShared
{
    public static class JwtConfiguration
    {
        public static string Issuer = "MyAuthServer";
        public static string Audience = "MyAuthClient";
        public static string Key = "qwerty7894561256789";

        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.ASCII.GetBytes(Key));
    }
}