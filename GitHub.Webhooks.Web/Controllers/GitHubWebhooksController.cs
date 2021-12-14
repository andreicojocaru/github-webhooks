using Microsoft.AspNetCore.Mvc;

namespace GitHub.Webhooks.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitHubWebhooksController : ControllerBase
    {
        [HttpPost]
        public void Webhooks()
        {

        }
    }
}
