using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OfficeControlSystemApi.Services
{
    public class AuthOptions
    {
        public const string AUTH_OPTIONS = "AuthOptions";
        public string ISSUER { get; set; } = string.Empty;
        public string AUDIENCE { get; set; } = string.Empty;
        public string KEY { get; set; } = string.Empty;

        public SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
