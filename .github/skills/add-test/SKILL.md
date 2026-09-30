---
name: add-test
description: "Use when writing, adding, or updating NUnit unit or integration tests in Dashagram. Follow the repository's Moq, FluentAssertions, naming, layout, and mirrored-folder conventions."
---

# Add Tests

Write focused tests that match the existing Dashagram test projects and verify observable behavior.

## Test Project Selection

- Put Application handler and business-logic unit tests in `tests/Dashagram.UnitTests/`.
- Put API and Infrastructure integration tests in `tests/Dashagram.IntegrationTests/`. Use the real application host and an isolated test database when testing persistence; do not use a developer or production database.
- Keep unit tests independent of the database by mocking repository interfaces with Moq.

## Folder Structure

Mirror the source project's feature and responsibility folders beneath the matching test project:

- `src/Dashagram.Application/Features/Posts/Queries/` maps to `tests/Dashagram.UnitTests/Features/Posts/Queries/`.
- `src/Dashagram.Application/Features/Posts/Commands/` maps to `tests/Dashagram.UnitTests/Features/Posts/Commands/`.
- Common application behaviors map under `tests/Dashagram.UnitTests/Common/Behaviours/`.
- API integration tests go under `tests/Dashagram.IntegrationTests/Api/`, grouped further by endpoint or resource where useful.

Name test files after the class or behavior under test, ending in `Tests.cs`.

## Test Names

Use this pattern:

```text
FunctionName_Scenario_ExpectedResult
```

Use the function or method name first, followed by the scenario and expected outcome. For example:

```csharp
Handle_WhenPostExists_ReturnsMappedPost
Handle_WhenPostDoesNotExist_ReturnsNotFoundResult
```

## Test Body Layout

Keep each test body in this order, with a blank line between each section:

1. Declare the variables and in-memory models needed by the test.
2. Configure Moq mocks for the scenario. If no dependency needs mocking, omit this section.
3. Call the function or method under test and store its result.
4. Assert the observable result with FluentAssertions.

Example:

```csharp
[Test]
public async Task Handle_WhenPostExists_ReturnsMappedPost()
{
    var postId = Guid.NewGuid();
    var post = new Post { Title = "A day at the park" };

    var repository = new Mock<IPostRepository>(MockBehavior.Strict);
    repository
        .Setup(repo => repo.GetPostByIdAsync(postId, It.IsAny<CancellationToken>()))
        .ReturnsAsync(post);
    var handler = new GetPostByIdQueryHandler(repository.Object);

    var result = await handler.Handle(new GetPostByIdQuery(postId), CancellationToken.None);

    result.Success.Should().BeTrue();
    result.Data.Should().NotBeNull();
    result.Data!.Title.Should().Be(post.Title);
    repository.Verify(repo => repo.GetPostByIdAsync(postId, It.IsAny<CancellationToken>()), Times.Once);
}
```

Use strict mocks when practical, set up only the calls required by the scenario, and verify important interactions when they are part of the behavior. Prefer direct FluentAssertions checks of result values over asserting implementation details.

## Validation

Run the narrowest relevant test project first, then the full suite when changes affect shared behavior or project configuration:

```powershell
dotnet test tests/Dashagram.UnitTests/Dashagram.UnitTests.csproj
dotnet test tests/Dashagram.IntegrationTests/Dashagram.IntegrationTests.csproj
dotnet test src/Dashagram.slnx
```