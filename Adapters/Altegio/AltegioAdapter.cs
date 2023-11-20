using CrmIntegration.Adapters.Altegio.Models;
using CrmIntegration.Options;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace CrmIntegration.Adapters.Altegio
{
    public class AltegioAdapter : IAltegioAdapter
    {
        private readonly HttpClient _httpClient;

        public AltegioAdapter(IOptions<AltegioIntegrationOptions> options)
        {
            _httpClient = new HttpClient(new AuthenticationHandler(this, options))
            {
                BaseAddress = new Uri(options.Value.Uri)
            };
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.api.v2+json");
        }

        public async Task<LoginOutput> LoginAsync(LoginInput input, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(input),
                RequestUri = new Uri("/api/v1/auth", UriKind.Relative),
                Method = HttpMethod.Post
            };

            var response = await _httpClient.SendAsync(request, cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<LoginRoot>(cancellationToken: cancellationToken);
            return result?.Data;
        }

        public async Task<IList<ClientOutput>> GetClientsAsync(GetClientsInput input, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(input),
                RequestUri = new Uri($"/api/v1/company/{input.CompanyId}/clients/search", UriKind.Relative),
                Method = HttpMethod.Post
            };

            var response = await _httpClient.SendAsync(request, cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<ClientsRoot>(cancellationToken: cancellationToken);
            return result?.Data;
        }

        public async Task UpdateClientAsync(UpdateClientInput input, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(input),
                RequestUri = new Uri($"/api/v1/client/{input.CompanyId}/{input.Id}", UriKind.Relative),
                Method = HttpMethod.Put
            };

            _ = await _httpClient.SendAsync(request, cancellationToken);
        }

        public async Task<ClientOutput> CreateClientAsync(CreateClientInput input, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(input),
                RequestUri = new Uri($"/api/v1/clients/{input.CompanyId}", UriKind.Relative),
                Method = HttpMethod.Post
            };

            var response = await _httpClient.SendAsync(request, cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<CreateClientRoot>(cancellationToken: cancellationToken);
            return result?.Data;
        }

        public async Task<IList<CustomerSubscriptionOutput>> GetCustomerSubscriptionsAsync(CustomerSubscriptionInput input, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(input),
                RequestUri = new Uri($"/api/v1/loyalty/abonements?company_id={input.CompanyId}&phone={input.Phone}", UriKind.Relative),
                Method = HttpMethod.Get
            };

            var response = await _httpClient.SendAsync(request, cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<CustomerSubscriptionRoot>(cancellationToken: cancellationToken);
            return result?.Data;
        }

        public async Task<ClientOutput> GetClientAsync(GetClientInput input, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(input),
                RequestUri = new Uri($"/api/v1/client/{input.CompanyId}/{input.Id}", UriKind.Relative),
                Method = HttpMethod.Get
            };

            var response = await _httpClient.SendAsync(request, cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<ClientRoot>(cancellationToken: cancellationToken);
            return result?.Data;
        }

        public async Task CreateGoodsTransactionAsync(CreateGoodsTransactionInput input, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(input),
                RequestUri = new Uri($"/api/v1/storage_operations/goods_transactions/{input.CompanyId}", UriKind.Relative),
                Method = HttpMethod.Post
            };

            _ = await _httpClient.SendAsync(request, cancellationToken);
        }

        public async Task<CreateDocumentOutput> CreateDocumentAsync(CreateDocumentInput input, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(input),
                RequestUri = new Uri($"/api/v1/storage_operations/documents/{input.CompanyId}", UriKind.Relative),
                Method = HttpMethod.Post
            };

            var response = await _httpClient.SendAsync(request, cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<CreateDocumentRoot>(cancellationToken: cancellationToken);
            return result?.Data;
        }
    }
}
