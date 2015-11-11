using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Converters;

namespace CT.Repository.Infrastructure
{
    public class IsoDateTimeZoneConverter : IsoDateTimeConverter
    {
        public IsoDateTimeZoneConverter()
        {
            DateTimeFormat = "yyyy-MM-ddTHH\\:mm\\:sszzz";
        }
    }
}