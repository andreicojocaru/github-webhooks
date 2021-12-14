using GitHub.Webhooks.Client;
using GitHub.Webhooks.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Octokit;

namespace GitHub.Webhooks.Web.Controllers
{
    [ApiController]
    [Route("github/webhooks")]
    public class GitHubWebhooksController : ControllerBase
    {
        private GitHubClientProvider gitHubClientProvider;

        public GitHubWebhooksController(GitHubClientProvider clientProvider)
        {
            gitHubClientProvider = clientProvider;
        }

        [Route("repository")]
        [HttpPost]
        public async Task<IActionResult> Repository([FromBody] GitHubWebhookRepositoryModel model)
        {
            Console.Write(model.Repository.Name);

            var issue = new NewIssue("Test Issue");
            issue.Body = "# This is a new issue from code.";

            var repository = await gitHubClientProvider.Client.Repository.Get(model.Repository.Id);

            var protection = new BranchProtectionSettingsUpdate(new BranchProtectionRequiredReviewsUpdate(true, true, 1));

            var settings = await gitHubClientProvider.Client.Repository.Branch.UpdateBranchProtection(repository.Id, "main", protection);

            //await gitHubClientProvider.Client.Issue.Create(repository.Id, issue);

            return Accepted();
        }
    }
}
