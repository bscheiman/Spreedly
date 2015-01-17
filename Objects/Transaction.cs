#region
using System;
using System.Xml.Linq;
using bscheiman.Common.Extensions;

#endregion

namespace Spreedly.Objects {
    public class Transaction : BaseObject {
        public bool Succeeded { get; set; }
        public string TransactionType { get; set; }
        public string State { get; set; }
        public string Message { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public Transaction(XElement elem) : base(elem) {
            Succeeded = Convert.ToBoolean(elem.Element("succeeded").ValueOrDefault());
            TransactionType = elem.Element("transaction_type").ValueOrDefault();
            State = elem.Element("state").ValueOrDefault();
            Message = elem.Element("message").ValueOrDefault();

            PaymentMethod = new PaymentMethod(elem.Element("payment_method"));
        }
    }
}