using GitHub.Webhooks.Client.Builders;
using NUnit.Framework;
using System;

namespace GitHub.Webhooks.Tests
{
    public class NewIssueBuilderTests
    {
        private NewIssueBuilder sut;

        [SetUp]
        public void Setup()
        {
            sut = new NewIssueBuilder();
        }

        [Test]
        public void NoTitle_ShouldThrowExceptionOnCreate()
        {
            // Assert
            Assert.Throws<InvalidOperationException>(() => sut.Create());
        }

        [Test]
        public void WithTitle_ShouldCreateIssue()
        {
            // Arrange
            var title = "Test Issue";

            // Act
            var issue = sut.WithTitle(title).Create();

            // Assert
            Assert.IsNotNull(issue);
            Assert.AreEqual(title, issue.Title);
            Assert.IsNull(issue.Body);
        }

        [Test]
        public void WithTitleAndBody_ShouldCreateIssue()
        {
            // Arrange
            var title = "Test Issue";
            var body = "#Test Header";

            // Act
            var issue = sut.WithTitle(title).WithBody(body).Create();

            // Assert
            Assert.IsNotNull(issue);
            Assert.AreEqual(title, issue.Title);
            Assert.AreEqual(body, issue.Body);
        }

        [Test]
        public void WithTitleAndMentions_ShouldCreateIssue()
        {
            // Arrange
            var title = "Test Issue";
            var body = "#Test Header";

            var user = "testuser";
            var expectedMention = "@testuser";

            // Act
            var issue = sut.WithTitle(title).WithBody(body).WithMentions(user).Create();

            // Assert
            Assert.IsNotNull(issue);
            Assert.IsNotNull(issue.Body);


            Assert.AreEqual(title, issue.Title);

            Assert.IsTrue(issue.Body.Contains(body));
            Assert.IsTrue(issue.Body.Contains(expectedMention));
        }

        [Test]
        public void WithTitleAndBodyAndMentions_ShouldCreateIssue()
        {
            // Arrange
            var title = "Test Issue";
            var user = "testuser";
            var expectedMention = "@testuser";

            // Act
            var issue = sut.WithTitle(title).WithMentions(user).Create();

            // Assert
            Assert.IsNotNull(issue);
            Assert.IsNotNull(issue.Body);

            Assert.AreEqual(title, issue.Title);
            Assert.IsTrue(issue.Body.Contains(expectedMention));
        }

        [Test]
        public void FromBranchProtectionBuilder_ShouldCreateIssueWithBody()
        {
            // Arrange
            var issueTitle = "Test Issue";
            var branchProtection = new BranchProtectionBuilder().WithEnforceAdmins(true);

            // Act 
            var issueBuilder = NewIssueBuilder.FromBranchProtection(branchProtection);
            var issue = issueBuilder?.WithTitle(issueTitle).Create();

            // Assert
            Assert.NotNull(issueBuilder);
            Assert.NotNull(issue);

            Assert.AreEqual(issueTitle, issue.Title);
            Assert.IsNotEmpty(issue.Body);
        }
    }
}