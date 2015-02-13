#region
using System;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using RestSharp;
using Spreedly.Objects;

#endregion

namespace Spreedly {
    public class SpreedlyLib {
        public const string BaseUrl = "https://core.spreedly.com/v1/";
        public string EnvironmentId { get; set; }
        public string Secret { get; set; }

        public SpreedlyLib(string environmentId, string secret) {
            EnvironmentId = environmentId;
            Secret = secret;
        }

        public Task<Gateway> AddGatewayAsync(string gateway) {
            return PostAsync<Gateway>("gateways.xml", string.Format("<gateway><gateway_type>{0}</gateway_type></gateway>", gateway));
        }

        public Task<Gateways> GetGatewayTokensAsync() {
            return GetAsync<Gateways>("gateways.xml");
        }

        public Task<Transaction> RedactAsync(string token) {
            return PutAsync<Transaction>("payment_methods/{payment_method_token}/redact.xml", "", new Parameter {
                Name = "payment_method_token",
                Value = token,
                Type = ParameterType.UrlSegment
            });
        }

        public Task<Transaction> RetainAsync(string token) {
            return PutAsync<Transaction>("payment_methods/{payment_method_token}/retain.xml", "", new Parameter {
                Name = "payment_method_token",
                Value = token,
                Type = ParameterType.UrlSegment
            });
        }

        public Task<Transaction> TransactionAsync(string gateway, string token, string currency, int amount) {
            return PostAsync<Transaction>("gateways/{gateway_token}/purchase.xml",
                string.Format(
                    "<transaction><payment_method_token>{0}</payment_method_token><amount>{1}</amount><currency_code>{2}</currency_code></transaction>",
                    token, amount, currency), new Parameter {
                        Name = "gateway_token",
                        Value = gateway,
                        Type = ParameterType.UrlSegment
                    });
        }

        public Task<Transaction> TransactionAsync(string gateway, string token, string currency, float amount) {
            return TransactionAsync(gateway, token, currency, (int) amount * 100);
        }

        #region Helpers
        internal Task<T> DeleteAsync<T>(string url, params Parameter[] parameters) where T : BaseObject {
            var tcs = new TaskCompletionSource<T>();
            var client = GetClient(url);

            client.ExecuteAsync(GetRequest(url, Method.DELETE, null, parameters), response => SetResult(response, tcs));

            return tcs.Task;
        }

        internal Task<T> GetAsync<T>(string url, string obj = "", params Parameter[] parameters) where T : BaseObject {
            var tcs = new TaskCompletionSource<T>();
            var client = GetClient(url);

            client.ExecuteAsync(GetRequest(url, Method.GET, obj, parameters), response => SetResult(response, tcs));

            return tcs.Task;
        }

        private static void SetResult<T>(IRestResponse response, TaskCompletionSource<T> tcs) where T : BaseObject {
            if (response.ErrorException == null)
                tcs.SetResult((T) Activator.CreateInstance(typeof (T), XDocument.Parse(response.Content).Root));
            else
                tcs.SetException(response.ErrorException);
        }

        internal RestClient GetClient(string url) {
            var client = new RestClient(BaseUrl) {
                UserAgent = "Spreedly.NET"
            };

            return client;
        }

        internal RestRequest GetRequest(string url, Method method, string xml = "", params Parameter[] parameters) {
            var request = new RestRequest(url, method) {
                Credentials = new NetworkCredential(EnvironmentId, Secret)
            };

            foreach (var p in parameters)
                request.AddParameter(p);

            request.AddParameter("application/xml", xml, ParameterType.RequestBody);

            return request;
        }

        internal Task<T> PostAsync<T>(string url, string obj = "", params Parameter[] parameters) where T : BaseObject {
            var tcs = new TaskCompletionSource<T>();
            var client = GetClient(url);

            client.ExecuteAsync(GetRequest(url, Method.POST, obj, parameters), response => SetResult(response, tcs));

            return tcs.Task;
        }

        internal Task<T> PutAsync<T>(string url, string obj = "", params Parameter[] parameters) where T : BaseObject {
            var tcs = new TaskCompletionSource<T>();
            var client = GetClient(url);

            client.ExecuteAsync(GetRequest(url, Method.PUT, obj, parameters), response => SetResult(response, tcs));

            return tcs.Task;
        }
        #endregion
    }
}