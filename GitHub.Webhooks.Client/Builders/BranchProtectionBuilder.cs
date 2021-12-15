using Octokit;
using System.Text;

namespace GitHub.Webhooks.Client.Builders
{
    public class BranchProtectionBuilder
    {
        private bool? enforceAdmins;
        private bool? dismissStaleReviews;
        private bool? codeOwnersRequiredReviews;
        private int? requiredReviewersCount;

        public BranchProtectionBuilder WithEnforceAdmins(bool value)
        {
            enforceAdmins = value;
            return this;
        }

        public BranchProtectionBuilder WithDismissStaleReviews(bool value)
        {
            dismissStaleReviews = value;
            return this;
        }

        public BranchProtectionBuilder WithCodeOwnersRequiredReviews(bool value)
        {
            codeOwnersRequiredReviews = value;
            return this;
        }

        public BranchProtectionBuilder WithRequiredReviewerCount(int count)
        {
            requiredReviewersCount = count;
            return this;
        }

        public BranchProtectionSettingsUpdate Create()
        {
            var reviews = new BranchProtectionRequiredReviewsUpdate(dismissStaleReviews ?? false, codeOwnersRequiredReviews ?? false, requiredReviewersCount ?? 0);
            var settings = new BranchProtectionSettingsUpdate(reviews)
            {
                EnforceAdmins = enforceAdmins ?? false
            };

            return settings;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("## Branch Protection (Reviews)");
            stringBuilder.AppendLine($"- Enforce Admins: `{enforceAdmins ?? false}`");
            stringBuilder.AppendLine($"- Dismiss stale reviews: `{dismissStaleReviews ?? false}`");
            stringBuilder.AppendLine($"- Code Owners required reviews: `{codeOwnersRequiredReviews ?? false}`");
            stringBuilder.AppendLine($"- Required reviewers count: `{requiredReviewersCount ?? 0}`");

            return stringBuilder.ToString();
        }
    }
}
