using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CateringSystem.PayPal
{
    public class PayPalClient : IDisposable
    {
        private AuthenticationHeaderValue _AuthorizationHeader;
        private readonly HttpClient _Client;
        private bool _IsDisposed;

        public String AccessToken { get; private set; }
        public bool IsSandbox { get; }

        public PayPalClient(PayPalOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof (options));
            }

            this._AuthorizationHeader = new AuthenticationHeaderValue(
                "Basic",

                Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(
                    $"{options.ClientId}:{options.Secret}"
                ))
            );

            this._Client = new HttpClient();
            this.IsSandbox = options.IsSandbox;
        }

        private void CheckState(bool requireAccessToken)
        {
            if (this._IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().FullName);
            }

            if (requireAccessToken && this.AccessToken is null)
            {
                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                this._Client.Dispose();
            }

            this._IsDisposed = true;
        }

        public async Task<String> GetAccessTokenAsync()
        {
            this.CheckState(false);

            HttpRequestMessage requestMessage = new HttpRequestMessage(
                HttpMethod.Post,
                this.IsSandbox ?
                    "https://api-m.sandbox.paypal.com/v1/oauth2/token" :
                    throw new NotImplementedException()
            );

            requestMessage.Headers.Add("Accept-Language", "en_GB");
            requestMessage.Headers.Authorization = this._AuthorizationHeader;

            requestMessage.Content = new FormUrlEncodedContent(
                new List<KeyValuePair<String, String>>
                {
                    new KeyValuePair<String, String>(
                        "grant_type",
                        "client_credentials"
                    )
                }
            );

            HttpResponseMessage responseMessage = await this._Client.SendAsync(requestMessage);

            if (responseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new PayPalException(PayPalException._GetTokenError, this.IsSandbox);
            }

            this.AccessToken = this.GetRootElement(await responseMessage.Content.ReadAsStringAsync(), "access_token");
            return this.AccessToken ?? throw new PayPalException(PayPalException._GetTokenError, this.IsSandbox);
        }

        private String GetRootElement(String jsonString, String name)
        {
            using (JsonDocument document = JsonDocument.Parse(jsonString))
            {
                foreach (JsonProperty property in document.RootElement.EnumerateObject())
                {
                    if (property.NameEquals(name))
                    {
                        return property.Value.GetString();
                    }
                }
            }

            return null;
        }
    }
}