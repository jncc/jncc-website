using JNCC.PublicWebsite.Core.Constants;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Extensions
{
    public static class TempDataDictionaryExtensions
    {
        public static void SetSuccessFlag(this TempDataDictionary tempData, string key = SuccessFlags.Default)
        {
            tempData.Add(key, true);
        }

        public static bool HasSuccessFlag(this TempDataDictionary tempData, string key = SuccessFlags.Default)
        {
            return tempData.ContainsKey(key) && tempData[key] != null;
        }
    }
}
