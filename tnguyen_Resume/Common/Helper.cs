using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace tnguyen_Resume.Common
{
    public static class Helper
    {
        public static string ValidatePhoneNumber(string phoneNumber)
        {
            var errorMessage = string.Empty;
            if (string.IsNullOrEmpty(phoneNumber.Trim())) return string.Empty;
            if (phoneNumber.Length > 11 || phoneNumber.Length < 8 || !Regex.IsMatch(phoneNumber, @"^[0-9]*$"))
            {
                errorMessage = "Phone number must be 8-11 numeric characters (" + phoneNumber + ")!";
            }
            return errorMessage;
        }
    }
}