using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHotel.Common.Extensions
{
    public static class StringExtensions
    {
        #region Public Methods

        /// <summary>
        /// Extracts the trailing id from the url
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The extracted id</returns>
        public static int ExtractIdFromUrl(this string url)
        {
            string[] parts = url.Split('/');

            if (parts.Length > 0)
            {
                // Extract the id from the last part
                int id = default(int);
                if (int.TryParse(parts[parts.Length - 1], out id))
                {
                    return id;
                }
            }

            return default(int);
        }

        /// <summary>
        /// Converts a string into a Base64 encoded string
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <returns>The base64 encoded string</returns>
        public static string Base64Encode(this string source)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(source));
        }

        /// <summary>
        /// Decodes a base64 encoded string
        /// </summary>
        /// <param name="encodedValue">The encoded string.</param>
        /// <returns>
        /// The base64 encoded string
        /// </returns>
        public static string Base64Decode(this string encodedValue)
        {
            string decodedValue = string.Empty;

            try
            {
                decodedValue = Encoding.UTF8.GetString(Convert.FromBase64String(encodedValue));
            }
            catch (FormatException)
            {
                // The string we attempted to decode was not in correct base64 format
                decodedValue = string.Empty;
            }

            return decodedValue;
        }

        /// <summary>
        /// Splits the string into an array using the specified delimiterzz
        /// </summary>
        /// <param name="value">The value to split into an array</param>
        /// <param name="delimiter">The delimiter for array items</param>
        /// <returns>The array</returns>
        public static string[] ToArray(this string value, char delimiter)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new string[0];
            }

            return value.Split(delimiter);
        }

        /// <summary>
        /// Appends the time stamp to file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The file name with appended time stamp</returns>
        public static string AppendTimeStampToFile(this string fileName)
        {
            return string.Concat(
                Path.GetFileNameWithoutExtension(fileName),
                DateTime.Now.ToString("yyyyMMddHHmmss"),
                Path.GetExtension(fileName));
        }

        #endregion
    }
}
