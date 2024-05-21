using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace assessment_platform_developer.Common.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex(@"^(?:\+1)?\s?\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$");
            return regex.IsMatch(phoneNumber);
        }

        public static bool IsValidEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.IsMatch(email);
        }
        public static bool IsValidCanadianZiCode(string canadianZip)
        {
            Regex regex = new Regex(@"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$");
            return regex.IsMatch(canadianZip);
        }
        public static bool IsValidUsZiCode(string usZip)
        {
            Regex regex = new Regex(@"^\d{5}(-\d{4})?$");
            return regex.IsMatch(usZip);
        }
    }
}