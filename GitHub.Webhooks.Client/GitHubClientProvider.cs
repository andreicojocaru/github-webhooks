using Microsoft.Extensions.Options;
using Octokit;
using System;
using System.Threading.Tasks;

namespace GitHub.Webhooks.Client
{
    public class GitHubClientProvider
    {
        private readonly string clientId;
        private readonly string clientSecret;
        
        public GitHubClient Client;

        public GitHubClientProvider(IOptions<GitHubSettings> settings)
        {
            clientId = settings.Value.ClientId ?? throw new ArgumentNullException(nameof(settings.Value.ClientId));
            clientSecret = settings.Value.ClientSecret ?? throw new ArgumentNullException(nameof(settings.Value.ClientSecret));

            Client = new GitHubClient(new ProductHeaderValue("TestOrg-Authentication"));
        }

        public Uri GetLoginUri()
        {
            var request = new OauthLoginRequest(clientId)
            {
                Scopes = { "user", "repo" }
            };

            return Client.Oauth.GetGitHubLoginUrl(request);
        }

        public async Task CreateAccessTokenAsync(string code)
        {
            var request = new OauthTokenRequest(clientId, clientSecret, code);
            var token = await Client.Oauth.CreateAccessToken(request);

            if (string.IsNullOrEmpty(token.AccessToken))
            {
                throw new InvalidOperationException("Could not retrieve Access Token for provided Authorization Code.");
            }

            var credentials = new Credentials(token.AccessToken);
            Client.Credentials = credentials;
        }
    }
}
