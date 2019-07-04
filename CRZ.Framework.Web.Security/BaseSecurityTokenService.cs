using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;

namespace CRZ.Framework.Web.Security
{
    public abstract class BaseSecurityTokenService<T> : ISecurityTokenService<T>
        where T : class
    {
        readonly SigninConfiguration _signinConfiguration;
        readonly TokenConfiguration _tokenConfiguration;

        protected BaseSecurityTokenService(SigninConfiguration signinConfiguration, TokenConfiguration tokenConfiguration)
        {
            _signinConfiguration = signinConfiguration ?? throw new ArgumentNullException(nameof(signinConfiguration));
            _tokenConfiguration = tokenConfiguration ?? throw new ArgumentNullException(nameof(tokenConfiguration));
        }

        protected abstract string GetGenericIdentity(T data);

        protected abstract IEnumerable<Claim> GetClaims(T data);

        public virtual string CreateToken(T data)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(GetGenericIdentity(data)),
                GetClaims(data));
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signinConfiguration.Credentials,
                Subject = identity,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now + TimeSpan.FromSeconds(_tokenConfiguration.Seconds)
            });
            var token = handler.WriteToken(securityToken);

            return token;
        }
    }
}
