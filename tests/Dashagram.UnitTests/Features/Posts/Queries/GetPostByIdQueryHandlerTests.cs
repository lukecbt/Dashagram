using Dashagram.Application.Common.Interfaces.Repositories;
using Dashagram.Application.Common.Interfaces.Services;
using Dashagram.Application.Common.Models.Storage;
using Dashagram.Application.Features.Posts.Queries;
using Dashagram.Domain.Models.Entities;
using FluentAssertions;
using Moq;

namespace Dashagram.UnitTests.Features.Posts.Queries;

public class GetPostByIdQueryHandlerTests
{
    [Test]
    public async Task Handle_WhenPostExists_ReturnsMappedPost()
    {
        var postId = Guid.NewGuid();
        var post = new Post
        {
            Description = "Playing fetch",
            UserId = "user-123",
            Images = [new PostImage { Key = "https://example.com/dog.jpg" }],
            Comments = [],
            Likes = []
        };

        var repository = new Mock<IPostRepository>(MockBehavior.Strict);
        repository
            .Setup(repo => repo.GetPostByIdAsync(postId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(post);
        var storage = new Mock<IStorageService>(MockBehavior.Strict);
        storage.Setup(s => s.GetPublicUrl(It.IsAny<string>()))
            .Returns((string key) => key);

        var handler = new GetPostByIdQueryHandler(repository.Object, storage.Object);
        var result = await handler.Handle(new GetPostByIdQuery(postId), CancellationToken.None);

        result.Success.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.Description.Should().Be(post.Description);
        result.Data.UserId.Should().Be(post.UserId);
        result.Data.Images.Should().ContainSingle()
            .Which.Url.Should().Be("https://example.com/dog.jpg");
        repository.Verify(repo => repo.GetPostByIdAsync(postId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_WhenPostDoesNotExist_ReturnsNotFoundResult()
    {
        var postId = Guid.NewGuid();
        
        var repository = new Mock<IPostRepository>(MockBehavior.Strict);
        repository
            .Setup(repo => repo.GetPostByIdAsync(postId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Post?)null);
        var storage = new Mock<IStorageService>(MockBehavior.Strict);
        storage.Setup(s => s.GetPublicUrl(It.IsAny<string>()))
            .Returns((string key) => key);

        var handler = new GetPostByIdQueryHandler(repository.Object, storage.Object);
        var result = await handler.Handle(new GetPostByIdQuery(postId), CancellationToken.None);

        result.Success.Should().BeFalse();
        result.Code.Should().Be("NotFound");
        repository.Verify(repo => repo.GetPostByIdAsync(postId, It.IsAny<CancellationToken>()), Times.Once);
    }
}