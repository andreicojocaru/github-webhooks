using GitHub.Webhooks.Client.Builders;
using NUnit.Framework;

namespace GitHub.Webhooks.Tests
{
    public class BranchProtectionBuilderTests
    {
        private BranchProtectionBuilder sut;

        [SetUp]
        public void Setup()
        {
            sut = new BranchProtectionBuilder();
        }

        [Test]
        public void DefaultSettings_ShouldCreateUpdateRequest()
        {
            // Act 
            var request = sut.Create();

            // Assert
            Assert.NotNull(request);
            Assert.NotNull(request.RequiredPullRequestReviews);

            Assert.IsFalse(request.EnforceAdmins);
            Assert.IsFalse(request.RequiredPullRequestReviews.DismissStaleReviews);
            Assert.IsFalse(request.RequiredPullRequestReviews.RequireCodeOwnerReviews);
            Assert.AreEqual(0, request.RequiredPullRequestReviews.RequiredApprovingReviewCount);
        }

        [Test]
        public void WithEnforceAdmins_ShouldCreateUpdateRequest()
        {
            // Arrange
            sut.WithEnforceAdmins(true);

            // Act 
            var request = sut.Create();

            // Assert
            Assert.IsTrue(request.EnforceAdmins);
        }

        [Test]
        public void WithDismissStaleReviews_ShouldCreateUpdateRequest()
        {
            // Arrange
            sut.WithDismissStaleReviews(true);

            // Act 
            var request = sut.Create();

            // Assert
            Assert.NotNull(request.RequiredPullRequestReviews);
            Assert.IsTrue(request.RequiredPullRequestReviews.DismissStaleReviews);
        }

        [Test]
        public void WithCodeOwnersRequiredReviews_ShouldCreateUpdateRequest()
        {
            // Arrange
            sut.WithCodeOwnersRequiredReviews(true);

            // Act 
            var request = sut.Create();

            // Assert
            Assert.NotNull(request.RequiredPullRequestReviews);
            Assert.IsTrue(request.RequiredPullRequestReviews.RequireCodeOwnerReviews);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void WithCodeOwnersRequiredReviews_ShouldCreateUpdateRequest(int amount)
        {
            // Arrange
            sut.WithRequiredReviewerCount(amount);

            // Act 
            var request = sut.Create();

            // Assert
            Assert.NotNull(request.RequiredPullRequestReviews);
            Assert.AreEqual(amount, request.RequiredPullRequestReviews.RequiredApprovingReviewCount);
        }

        [Test]
        public void ToString_ShouldReturnInternalStateString()
        {
            // Arrange
            sut.WithDismissStaleReviews(true).WithEnforceAdmins(true);

            // Act 
            var serialized = sut.ToString();

            // Assert
            Assert.NotNull(serialized);
            Assert.IsTrue(serialized.Contains("Enforce Admins: `True`"));
            Assert.IsTrue(serialized.Contains("Dismiss stale reviews: `True`"));
        }
    }
}