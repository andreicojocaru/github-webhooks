using GitHub.Webhooks.Client.Builders;
using System;
using System.Threading.Tasks;

namespace GitHub.Webhooks.Client
{
    public class GitHubBranchProtectionService
    {
        private GitHubClientProvider gitHubClientProvider;

        public GitHubBranchProtectionService(GitHubClientProvider clientProvider)
        {
            gitHubClientProvider = clientProvider;
        }

        public async Task CreateDefaultBranchProtectionAsync(long repositoryId)
        {
            var repository = await gitHubClientProvider.Client.Repository.Get(repositoryId);

            if(repository == null)
            {
                throw new ArgumentException($"Repository {repositoryId} was not found!");
            }

            var user = await gitHubClientProvider.Client.User.Current();

            if(user == null)
            {
                throw new InvalidOperationException("GitHub client is missing valid User information!");
            }

            var branchProtectionBuilder = new BranchProtectionBuilder()
                .WithEnforceAdmins(true)
                .WithCodeOwnersRequiredReviews(true)
                .WithDismissStaleReviews(true)
                .WithRequiredReviewerCount(2);

            var branchProtection = branchProtectionBuilder.Create();

            var issueBuilder = NewIssueBuilder.FromBranchProtection(branchProtectionBuilder)
                .WithTitle("Branch Protection Summary")
                .WithMentions(user.Login);

            var issue = issueBuilder.Create();

            await gitHubClientProvider.Client.Repository.Branch.UpdateBranchProtection(repository.Id, repository.DefaultBranch, branchProtection);
            await gitHubClientProvider.Client.Issue.Create(repository.Id, issue);
        }
    }
}
