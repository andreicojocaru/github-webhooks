namespace GitHub.Webhooks.Web.Models
{
    public class GitHubModelConstants
    {
        public const string RepositoryCreated = "created";
    }

    public class GitHubWebhookRepositoryModel
    {
        public string Action { get; set; } 
        public GitHubRepository? Repository { get; set; }
    }

    public class GitHubRepository
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
