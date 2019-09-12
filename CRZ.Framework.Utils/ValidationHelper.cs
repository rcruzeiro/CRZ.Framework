using System;
using System.Text.RegularExpressions;

namespace CRZ.Framework.Utils
{
    public static class ValidationHelper
    {
        static readonly Lazy<Regex> _emailPattern =
            new Lazy<Regex>(() => new Regex(@"^([a-zA-Z0-9!#$%&'*+/=?^{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_{|}~-]+)*)@((?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9])(?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$", RegexOptions.IgnoreCase | RegexOptions.Compiled));

        public static Regex EmailPattern => _emailPattern.Value;

        public static class Common
        {
            public static bool IsEmail(string email)
            {
                if (string.IsNullOrWhiteSpace(email)) return false;

                return EmailPattern.IsMatch(email);
            }
        }
    }
}
