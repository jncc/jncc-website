using Newtonsoft.Json;

namespace JNCC.PublicWebsite.Core.Utilities
{
    internal static class JsonUtility
    {
        public static bool TryParseJson<T>(this string obj, out T result)
        {
            try
            {
                // Validate missing fields of object
                var settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                };

                result = JsonConvert.DeserializeObject<T>(obj, settings);
                return true;
            }
            catch
            {
                result = default(T);
                return false;
            }
        }
    }
}
