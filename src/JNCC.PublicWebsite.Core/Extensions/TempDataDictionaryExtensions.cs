using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Extensions
{
    public static class TempDataDictionaryExtensions
    {
        public static void SetSuccessFlag(this TempDataDictionary tempData, string key)
        {
            tempData.Add(key, true);
        }

        public static bool HasSuccessFlag(this TempDataDictionary tempData, string key)
        {
            return tempData.ContainsKey(key) && tempData[key] != null;
        }
    }
}
