using Dashagram.Application.Common.Interfaces.Repositories;
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
            Title = "A day at the park",
            Description = "Playing fetch",
            UserId = "user-123",
            Images = [new PostImage { Url = "https://example.com/dog.jpg" }],
            Comments = [],
            Likes = []
        };

        var repository = new Mock<IPostRepository>(MockBehavior.Strict);
        repository
            .Setup(repo => repo.GetPostByIdAsync(postId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(post);
        var handler = new GetPostByIdQueryHandler(repository.Object);

        var result = await handler.Handle(new GetPostByIdQuery(postId), CancellationToken.None);

        result.Success.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data!.Title.Should().Be(post.Title);
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
        var handler = new GetPostByIdQueryHandler(repository.Object);

        var result = await handler.Handle(new GetPostByIdQuery(postId), CancellationToken.None);

        result.Success.Should().BeFalse();
        result.Code.Should().Be("NotFound");
        repository.Verify(repo => repo.GetPostByIdAsync(postId, It.IsAny<CancellationToken>()), Times.Once);
    }
}