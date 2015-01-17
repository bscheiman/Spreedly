#region
using System.Linq;
using System.Xml.Linq;

#endregion

namespace Spreedly.Objects {
    public class Gateways : BaseObject {
        public Gateway[] Tokens { get; set; }

        public Gateways(XElement elem) : base(elem) {
            Tokens = elem.Elements("gateway").Select(b => new Gateway(b)).ToArray();

            CreatedAt = Tokens.Min(t => t.CreatedAt);
            UpdatedAt = Tokens.Max(t => t.UpdatedAt);
        }
    }
}