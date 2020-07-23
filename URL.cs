using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace THelper
{
    public class URL
    {
        /// <summary>
        ///     Получает имя файла из URL
        /// </summary>
        /// <param name="adress">URL-адресс</param>
        /// <returns>Имя файла</returns>
        public static string GetFileName(string adress)
        {
            var matches = Regex.Matches(adress, @"/").Cast<Match>().Select(i => i.Value).ToArray();
            var slash = string.Join("", matches);
            var filename = adress.Split('/')[slash.Length];
            return filename;
        }

        /// <summary>
        ///     Получает имя файла из URL
        /// </summary>
        /// <param name="adress">URL-адресс в формате URI</param>
        /// <returns>Имя файла</returns>
        public static string GetFileName(Uri adress)
        {
            var matches = Regex.Matches(adress.ToString(), @"/").Cast<Match>().Select(i => i.Value).ToArray();
            var slash = string.Join("", matches);
            var filename = adress.ToString().Split('/')[slash.Length];
            return filename;
        }
    }
}