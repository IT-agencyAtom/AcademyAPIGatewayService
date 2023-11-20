using CrmIntegration.Adapters.Altegio.Models;
using CrmIntegration.Options;
using CrmIntegration.Utils;
using Microsoft.Extensions.Options;
using System.Security.Authentication;

namespace CrmIntegration.Adapters.Altegio
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly IAltegioAdapter _altegioAdapter;
        private readonly AltegioIntegrationOptions _options;
        private const string AuthMethod = "/api/v1/auth";

        public AuthenticationHandler(IAltegioAdapter altegioAdapter, 
            IOptions<AltegioIntegrationOptions> options) : base(new HttpClientHandler())
        {
            _altegioAdapter = altegioAdapter;
            _options = options.Value;
        }

        private async Task<string> GetBearerTokenAsync(CancellationToken cancellationToken = default)
        {
            var tokens = FileManager.GetTokens<LoginOutput>(Integrations.Altegio);
            if (tokens == null)
            {
                var loginInput = new LoginInput
                {
                    Login = _options.Username,
                    Password = _options.Password
                };
                tokens = await _altegioAdapter.LoginAsync(loginInput, cancellationToken);
                if (tokens == null)
                {
                    throw new AuthenticationException();
                }
                else
                {
                    FileManager.SaveTokens(Integrations.Altegio, tokens);
                }
            }

            return $"Bearer {_options.PartnerToken}, User {tokens.UserToken}";
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.LocalPath == AuthMethod)
            {
                request.Headers.Add("Authorization", $"Bearer {_options.PartnerToken}");
                return await base.SendAsync(request, cancellationToken);
            }

            var bearerToken = await GetBearerTokenAsync(cancellationToken);
            request.Headers.TryAddWithoutValidation("Authorization", bearerToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
