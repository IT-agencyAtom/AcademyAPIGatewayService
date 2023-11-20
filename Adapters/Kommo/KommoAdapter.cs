using CrmIntegration.Adapters.Kommo.Models;
using CrmIntegration.Options;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Security.Authentication;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CrmIntegration.Adapters.Kommo
{
    public class KommoAdapter : IKommoAdapter
    {
        private readonly HttpClient _httpClient;

        public KommoAdapter(IOptions<KommoIntegrationOptions> options)
        {
            _httpClient = new HttpClient(new AuthenticationHandler(this, options))
            {
                BaseAddress = new Uri(options.Value.SubUri)
            };
        }

        public async Task<AccessTokenOutput> GetTokensAsync(AccessTokenInput input, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(input),
                RequestUri = new Uri("/oauth2/access_token", UriKind.Relative),
                Method = HttpMethod.Post
            };
            var response = await _httpClient.SendAsync(request, cancellationToken);
            if (response?.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new AuthenticationException();
            }

            var result = await response.Content.ReadFromJsonAsync<AccessTokenOutput>(cancellationToken: cancellationToken);
            return result;
        }

        public async Task<IList<ContactOutput>> GetContactsAsync(GetContactsInput input, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"/api/v4/contacts?query={input.Query}&with={input.With}", UriKind.Relative),
                Method = HttpMethod.Get
            };

            var response = await _httpClient.SendAsync(request, cancellationToken);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return default;
            }

            var result = await response.Content.ReadFromJsonAsync<ContactRoot>(cancellationToken: cancellationToken);

            return result?.Embedded?.Contacts;
        }

        public async Task<LeadOutput> GetLeadByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"/api/v4/leads/{id}?with=contacts", UriKind.Relative),
                Method = HttpMethod.Get
            };

            var response = await _httpClient.SendAsync(request, cancellationToken);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return default;
            }

            var result = await response.Content.ReadFromJsonAsync<LeadOutput>(cancellationToken: cancellationToken);

            return result;
        }

        public async Task<IList<LeadOutput>> GetLeadsAsync(GetLeadsInput input, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"/api/v4/leads?with={input.With}&query={input.Query}", UriKind.Relative),
                Method = HttpMethod.Get
            };

            var response = await _httpClient.SendAsync(request, cancellationToken);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return default;
            }

            var result = await response.Content.ReadFromJsonAsync<LeadRoot>(cancellationToken: cancellationToken);

            return result?.Embedded?.Leads;
        }

        public async Task<ContactOutput> GetContactByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"/api/v4/contacts/{id}", UriKind.Relative),
                Method = HttpMethod.Get
            };

            var response = await _httpClient.SendAsync(request, cancellationToken);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return default;
            }

            var result = await response.Content.ReadFromJsonAsync<ContactOutput>(cancellationToken: cancellationToken);

            return result;
        }

        public async Task UpdateContactAsync(UpdateContactInput input, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(input),
                RequestUri = new Uri($"/api/v4/contacts/{input.Id}", UriKind.Relative),
                Method = HttpMethod.Patch
            };

            _ = await _httpClient.SendAsync(request, cancellationToken);
        }

        public async Task<IList<CreateComplexLeadOutput>> CreateComplexLeadAsync(IList<CreateComplexLeadInput> input, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(input),
                RequestUri = new Uri("/api/v4/leads/complex", UriKind.Relative),
                Method = HttpMethod.Post
            };

            var response = await _httpClient.SendAsync(request, cancellationToken);
            var result = await response.Content.ReadFromJsonAsync<IList<CreateComplexLeadOutput>>(cancellationToken: cancellationToken);

            return result;
        }

        public async Task CreateCatalogElementsAsync(CreateCatalogElementsInput input, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(input.Items, options: new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault
                }),
                RequestUri = new Uri($"/api/v4/catalogs/{input.CatalogId}/elements", UriKind.Relative),
                Method = HttpMethod.Post
            };

            _ = await _httpClient.SendAsync(request, cancellationToken);
        }

        public async Task CreateTaskAsync(IList<CreateTaskInput> input, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                Content = JsonContent.Create(input, options: new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault
                }),
                RequestUri = new Uri($"/api/v4/tasks", UriKind.Relative),
                Method = HttpMethod.Post
            };

            _ = await _httpClient.SendAsync(request, cancellationToken);
        }

        public async Task<CatalogElementOutput> GetCatalogElementAsync(GetCatalogElementInput input, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"/api/v4/catalogs/{input.CatalogId}/elements/{input.ElementId}", UriKind.Relative),
                Method = HttpMethod.Get
            };

            var response = await _httpClient.SendAsync(request, cancellationToken);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return default;
            }

            var result = await response.Content.ReadFromJsonAsync<CatalogElementOutput>(cancellationToken: cancellationToken);

            return result;
        }
    }
}
