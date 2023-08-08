using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Nop.Plugin.Shipping.UPS.Services;

namespace Nop.Plugin.Shipping.UPS.API.Rates
{
    public partial class RateClient
    {
        private UPSSettings _upsSettings;
        private string _accessToken;

        public RateClient(HttpClient httpClient, UPSSettings upsSettings, string accessToken) : this(httpClient)
        {
            _upsSettings = upsSettings;
            _accessToken = accessToken;

            if (!_upsSettings.UseSandbox)
                BaseUrl = UPSDefaults.ApiUrl;
        }

        partial void PrepareRequest(HttpClient client, HttpRequestMessage request,
            string url)
        {
            client.PrepareRequest(request, _upsSettings, _accessToken);
        }

        public async Task<RateResponse> ProcessRateAsync(RateRequest request)
        {
            var response = await RateAsync(Guid.NewGuid().ToString(),
                _upsSettings.UseSandbox ? "testing" : UPSDefaults.SystemName, string.Empty, "v1", "Shop", new RATERequestWrapper
            {
                RateRequest = request
            });

            return response.RateResponse;
        }

        partial void UpdateJsonSerializerSettings(JsonSerializerSettings settings)
        {
            settings.ContractResolver = new NullToEmptyStringResolver();
        }
    }

    public partial class RateResponse_RatedShipment
    {
        /// <summary>
        /// <remarks>
        /// For some reason, the description of this field in the API definition
        /// does not correspond to reality, so we had to remove this field from the
        /// automatically generated code and create the correct signature manually.
        ///
        /// Do not delete this field unless you have made sure that the description of the API
        /// or the response from the server has changed</remarks>
        /// </summary>
        [JsonProperty("RatedPackage", Required = Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public RatedShipment_RatedPackage RatedPackage { get; set; }
    }
}
