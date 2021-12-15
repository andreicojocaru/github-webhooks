namespace GitHub.Webhooks.Web.Models
{
    public class GitHubWebhookRepositoryModel
    {
        public GitHubRepository? Repository { get; set; }
    }

    public class GitHubRepository
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
