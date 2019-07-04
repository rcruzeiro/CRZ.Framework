using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CRZ.Framework.Web.Security
{
    public class SigninConfiguration
    {
        public SecurityKey Key { get; }

        public SigningCredentials Credentials { get; }

        public SigninConfiguration(string secureString)
        {
            Key = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(secureString));

            Credentials = new SigningCredentials(
                Key, SecurityAlgorithms.HmacSha256Signature);
        }
    }
}
