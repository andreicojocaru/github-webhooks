using GitHub.Webhooks.Client;
using GitHub.Webhooks.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace GitHub.Webhooks.Web.Controllers
{
    [ApiController]
    [Route("github/webhooks")]
    public class GitHubWebhooksController : ControllerBase
    {
        private readonly GitHubBranchProtectionService branchProtectionService;

        public GitHubWebhooksController(GitHubBranchProtectionService branchProtectionService)
        {
            this.branchProtectionService = branchProtectionService;
        }

        [Route("repository")]
        [HttpPost]
        public async Task<IActionResult> Repository([FromBody] GitHubWebhookRepositoryModel model)
        {
            if (model.Repository == null)
            {
                return BadRequest("Repository is missing in the Webhook body");
            }

            if(model.Action != GitHubModelConstants.RepositoryCreated)
            {
                return NoContent();
            }

            await branchProtectionService.CreateDefaultBranchProtectionAsync(model.Repository.Id);
            return Accepted();
        }
    }
}
