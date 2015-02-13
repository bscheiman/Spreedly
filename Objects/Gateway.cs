#region
using System.Xml.Linq;
using bscheiman.Common.Extensions;

#endregion

namespace Spreedly.Objects {
    public class Gateway : BaseObject {
        public string Name { get; set; }

        public Gateway() : base(null) {
        }

        public Gateway(XElement elem) : base(elem) {
            Name = elem.Element("name").ValueOrDefault();
            Token = elem.Element("token").ValueOrDefault();
        }
    }
}