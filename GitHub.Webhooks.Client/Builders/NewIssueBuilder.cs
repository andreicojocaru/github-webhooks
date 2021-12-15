using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GitHub.Webhooks.Client.Builders
{
    public class NewIssueBuilder
    {
        private string? body;
        private string? title;
        private IList<string>? mentions;

        public static NewIssueBuilder FromBranchProtection(BranchProtectionBuilder builder)
        {
            var body = builder.ToString();
            return new NewIssueBuilder().WithBody(body);
        }

        public NewIssueBuilder WithBody(string value)
        {
            body = value;
            return this;
        }

        public NewIssueBuilder WithTitle(string value)
        {
            title = value;
            return this;
        }

        public NewIssueBuilder WithMentions(params string[] users)
        {
            mentions = users.ToList();
            return this;
        }

        public NewIssue Create()
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new InvalidOperationException("Cannot build NewIssue without Title. Call WithTitle() first.");
            }

            if (mentions != null && mentions.Any())
            {
                var newBody = body ?? string.Empty;

                var mentionsString = SerializeMentions();
                newBody += mentionsString;

                body = newBody;
            }

            return new NewIssue(title)
            {
                Body = body
            };
        }

        private string SerializeMentions()
        {
            var users = string.Join(Environment.NewLine, mentions.Select(user => $"@{user}"));

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("## Mentions");
            stringBuilder.AppendLine(users);

            return stringBuilder.ToString();
        }
    }
}
