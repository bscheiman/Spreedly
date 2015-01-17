#region
using System;
using System.Globalization;
using System.Xml.Linq;
using bscheiman.Common.Extensions;

#endregion

namespace Spreedly.Objects {
    public class BaseObject {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Token { get; set; }

        public BaseObject(XElement elem) {
            if (elem == null)
                return;

            Token = elem.Element("token").ValueOrDefault();
            CreatedAt = DateTime.ParseExact(elem.Element("created_at").ValueOrDefault("1970-01-01T00:00:01Z"), new[] {
                @"yyyy-MM-dd\THH:mm:ss\Z",
                @"yyyy-MM-dd\THH:mm:ssK"
            }, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal).ToUniversalTime();
            UpdatedAt = DateTime.ParseExact(elem.Element("updated_at").ValueOrDefault("1970-01-01T00:00:01Z"), new[] {
                @"yyyy-MM-dd\THH:mm:ss\Z",
                @"yyyy-MM-dd\THH:mm:ssK"
            }, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal).ToUniversalTime();
        }
    }
}