using Newtonsoft.Json.Converters;

namespace JNCC.PublicWebsite.Core.Converters
{
    public sealed class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
