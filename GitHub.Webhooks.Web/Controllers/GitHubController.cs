using GitHub.Webhooks.Client;
using Microsoft.AspNetCore.Mvc;
using Octokit;

namespace GitHub.Webhooks.Web.Controllers
{
    [ApiController]
    [Route("github")]
    public class GitHubController : ControllerBase
    {
        private GitHubClientProvider gitHubClientProvider;

        public GitHubController(GitHubClientProvider clientProvider)
        {
            gitHubClientProvider = clientProvider;
        }

        [Route("login")]
        [HttpGet]
        public IActionResult Login()
        {
            var redirectUri = gitHubClientProvider.GetLoginUri();
            return Redirect(redirectUri.AbsoluteUri);
        }

        [Route("authorize")]
        [HttpGet]
        public async Task<IActionResult> Authorize(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest();
            }

            await gitHubClientProvider.CreateAccessTokenAsync(code);

            return Redirect("/swagger");
        }
    }
}
