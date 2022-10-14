using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.Logging;
using FluentAssertions;

namespace Blazor.Blog.Api.Tests;

public class GetPostsFunctionTests
{
    [Theory]
    [InlineData("", typeof(BadRequestResult))]
    [InlineData("QueryParamValue", typeof(OkResult))]
    public async Task Function_Returns_Correct_Status_Code(string queryParam, Type expectedResult)
    {
        //Arrange
        var qc = new QueryCollection(new Dictionary<string, StringValues> { { "name", new StringValues(queryParam) } });
        var request = new Mock<HttpRequest>();
        request.Setup(x => x.Query)
            .Returns(() => qc);
        request.Setup(x => x.Body)
            .Returns(() => new MemoryStream());

        var logger = Mock.Of<ILogger>();
        //Act
        var response = await GetPostsFunction.Run(request.Object, logger);
        //Assert
        response.GetType().Should().BeOfType(expectedResult);
    }
}
