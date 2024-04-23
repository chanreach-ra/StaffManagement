using System.Text.RegularExpressions;
using StaffManagementAPI.Models;

namespace StaffManagementAPI.Extensions
{
    public static class GlobalExtensions
    {
        public static bool IsNull(this BaseObject obj)
        {
            return obj == null;
        }
        public static bool IsValidName(this string inputString, int maxLength)
        {
            // Check if the string contains only text (letters, spaces, etc.)
            if (!Regex.IsMatch(inputString, "^[a-zA-Z\\s]*$"))
            {
                return false;
            }

            // Check if the length of the string is at most {length} characters
            if (inputString.Length > maxLength)
            {
                return false;
            }

            return true;
        }
        public static bool IsValidID(this string inputString, int maxLength)
        {
            // Check if the string contains only letters and numbers
            if (!Regex.IsMatch(inputString, "^[a-zA-Z0-9]*$"))
            {
                return false;
            }

            // Check if the length of the string is at most maxLength characters
            if (inputString.Length != maxLength)
            {
                return false;
            }

            return true;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }
        public static string Append(this string message, string newMessage)
        {
            if (message.IsNullOrEmpty())
                message = newMessage;
            else
                message += " | " + newMessage;
            return message;
        }
        public static string ToEnumString<T>(this T type) where T : Enum
        {
            return Enum.GetName(typeof(T), type);
        }
    }
}