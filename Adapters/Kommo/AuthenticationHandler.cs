using CrmIntegration.Adapters.Kommo.Models;
using CrmIntegration.Options;
using CrmIntegration.Utils;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;

namespace CrmIntegration.Adapters.Kommo
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private const string RefreshTokenGrantType = "refresh_token";
        private const string AuthorizationCodeGrantType = "authorization_code";
        private const string OAuthMethod = "/oauth2/access_token";

        private readonly IKommoAdapter _kommoAdapter;
        private readonly KommoIntegrationOptions _options;

        public AuthenticationHandler(IKommoAdapter kommoAdapter, IOptions<KommoIntegrationOptions> options) : base(new HttpClientHandler())
        {
            _kommoAdapter = kommoAdapter;
            _options = options.Value;
        }

        private async Task<string> GetBearerTokenAsync(CancellationToken cancellationToken = default)
        {
            var tokens = FileManager.GetTokens<AccessTokenOutput>(Integrations.Kommo);
            if (tokens == null)
            {
                var accessTokenInput = new AccessTokenInput
                {
                    ClientId = _options.ClientId,
                    ClientSecret = _options.ClientSecret,
                    RedirectUri = _options.RedirectUri,
                    Code = _options.AuthorizationCode,
                    GrantType = AuthorizationCodeGrantType
                };
                tokens = await _kommoAdapter.GetTokensAsync(accessTokenInput, cancellationToken);
                if (tokens == null || tokens.AccessToken == null)
                {
                    throw new AuthenticationException();
                }
                else
                {
                    FileManager.SaveTokens(Integrations.Kommo, tokens);
                }
            }
            else
            {
                var refresh = false;

                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    JwtSecurityToken jwt = tokenHandler.ReadJwtToken(tokens.AccessToken);
                    if (jwt.ValidTo < DateTime.UtcNow)
                    {
                        refresh = true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    refresh = true;
                }

                if (refresh)
                {
                    try
                    {
                        var accessTokenInput = new AccessTokenInput
                        {
                            ClientId = _options.ClientId,
                            ClientSecret = _options.ClientSecret,
                            RedirectUri = _options.RedirectUri,
                            Code = _options.AuthorizationCode,
                            GrantType = RefreshTokenGrantType
                        };
                        tokens = await _kommoAdapter.GetTokensAsync(accessTokenInput, cancellationToken);
                        if (tokens == null)
                        {
                            throw new AuthenticationException();
                        }
                        else
                        {
                            FileManager.SaveTokens(Integrations.Kommo, tokens);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                        FileManager.ClearTokens(Integrations.Kommo);
                    }
                }
            }

            return $"{tokens.TokenType} {tokens.AccessToken}";
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.LocalPath == OAuthMethod)
            {
                return await base.SendAsync(request, cancellationToken);
            }

            var bearerToken = await GetBearerTokenAsync(cancellationToken);
            request.Headers.Add("Authorization", bearerToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
