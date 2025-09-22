using ConnectCareMobile.Common.Global.Enum;
using KellermanSoftware.CompareNetObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using ConnectCareMobile.Common.Attributes;

namespace ConnectCareMobile.Common.Global
{
    public static class AppExtensions
    {
        public static string GetEnumDescriptionString<T>(this T value) where T : IConvertible
        {

            DescriptionAttribute[] attributes = (DescriptionAttribute[])value
                .GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(ELT => (T)ELT.Clone()).ToList();
        }
        public static string Capitalize(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            string CapitalizedStr = "";
            CapitalizedStr = Char.ToUpper(str[0]).ToString();
            CapitalizedStr += str.Substring(1);
            return CapitalizedStr;
        }
        public static bool Compare<T>(this IList<T> initialList, IList<T> listToCompare) //where T : BaseEntity
        {
            CompareLogic listComparison = new CompareLogic();
            listComparison.Config.AttributesToIgnore.Add(typeof(CompareIgnoreAttribute));
            var comparisonResult = listComparison.Compare(initialList, listToCompare);
            return comparisonResult.AreEqual;
        }
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
                (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
        /// <summary>
        /// Extension for a collection to check if null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> source)
        {
            return source ?? Enumerable.Empty<T>();
        }
        public static string RemoveAllSpaces(this string initialString)
        {
            if (string.IsNullOrWhiteSpace(initialString))
                return null;
            return String.Concat(initialString.Where(c => !Char.IsWhiteSpace(c)));
        }
        public static Tuple<bool, string> GetDateFormatted(this DateTimeOffset dateTimeOffset, string dateFormatParam)
        {

            if (dateTimeOffset == null || dateTimeOffset == DateTimeOffset.MinValue)
                return new Tuple<bool, string>(false, string.Empty);
            if (dateTimeOffset.ToLocalTime().Date == DateTimeOffset.Now.ToLocalTime().Date)
                return new Tuple<bool, string>(true, "Today");
            if (dateTimeOffset.ToLocalTime().Date == DateTimeOffset.Now.Date.AddDays(-1))
                return new Tuple<bool, string>(true, "Yesterday");
            else
            {
                var dateTime = dateTimeOffset.ToLocalTime().DateTime;
                var dateFormat = dateFormatParam ?? GlobalSettings.DefaultDateTimeFormat;
                if (AppSharedPrefences.Instance.SelectedLanguage == (int)LanguageEnum.Ar)
                    return new Tuple<bool, string>(false, dateTime.ToString(dateFormat, new CultureInfo("ar-AE")));
                else
                    return new Tuple<bool, string>(false, dateTime.ToString(dateFormat));
            }
        }
    }
}
