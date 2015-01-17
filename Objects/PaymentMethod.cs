#region
using System;
using System.Xml.Linq;
using bscheiman.Common.Extensions;

#endregion

namespace Spreedly.Objects {
    public class PaymentMethod : BaseObject {
        public string Email { get; set; }
        public string Data { get; set; }
        public string StorageState { get; set; }
        public bool Test { get; set; }
        public string Last4 { get; set; }
        public string Bin { get; set; }
        public string CardType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public bool EligibleForCardUpdater { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Number { get; set; }
        public string VerificationValue { get; set; }
        public string PaymentMethodType { get; set; }
        public string ShippingPhoneNumber { get; set; }
        public string ShippingCountry { get; set; }
        public string ShippingZip { get; set; }
        public string ShippingState { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingAddress1 { get; set; }
        public string ShippingAddress2 { get; set; }

        public PaymentMethod(XElement elem) : base(elem) {
            Email = elem.Element("email").ValueOrDefault();
            Data = elem.Element("data").ValueOrDefault();
            StorageState = elem.Element("storage_state").ValueOrDefault();
            Test = Convert.ToBoolean(elem.Element("test").ValueOrDefault("false"));
            Last4 = elem.Element("last_four_digits").ValueOrDefault();
            Bin = elem.Element("first_six_digits").ValueOrDefault();
            CardType = elem.Element("card_type").ValueOrDefault();
            FirstName = elem.Element("first_name").ValueOrDefault();
            LastName = elem.Element("last_name").ValueOrDefault();
            ExpMonth = Convert.ToInt32(elem.Element("month").ValueOrDefault());
            ExpYear = Convert.ToInt32(elem.Element("year").ValueOrDefault());
            Address1 = elem.Element("address1").ValueOrDefault();
            Address2 = elem.Element("address2").ValueOrDefault();
            City = elem.Element("city").ValueOrDefault();
            State = elem.Element("state").ValueOrDefault();
            Zip = elem.Element("zip").ValueOrDefault();
            Country = elem.Element("country").ValueOrDefault();
            PhoneNumber = elem.Element("phone_number").ValueOrDefault();
            FullName = elem.Element("full_name").ValueOrDefault();
            EligibleForCardUpdater = Convert.ToBoolean(elem.Element("eligible_for_card_updater").ValueOrDefault());
            ShippingAddress1 = elem.Element("shipping_address1").ValueOrDefault();
            ShippingAddress2 = elem.Element("shipping_address2").ValueOrDefault();
            ShippingCity = elem.Element("shipping_city").ValueOrDefault();
            ShippingState = elem.Element("shipping_state").ValueOrDefault();
            ShippingZip = elem.Element("shipping_zip").ValueOrDefault();
            ShippingCountry = elem.Element("shipping_country").ValueOrDefault();
            ShippingPhoneNumber = elem.Element("shipping_phone_number").ValueOrDefault();
            PaymentMethodType = elem.Element("payment_method_type").ValueOrDefault();
            VerificationValue = elem.Element("verification_value").ValueOrDefault();
            Number = elem.Element("number").ValueOrDefault();
        }
    }
}