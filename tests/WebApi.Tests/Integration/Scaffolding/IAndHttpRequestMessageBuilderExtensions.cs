using MyTested.WebApi.Builders.Contracts.HttpRequests;

namespace WebApi.Tests.Integration.Scaffolding
{
    public static class IAndHttpRequestMessageBuilderExtensions
    {
        public static IAndHttpRequestMessageBuilder WithAuthorization(this IHttpRequestMessageBuilder builder) => builder.WithHeader("X-Testing", "true");
        public static IAndHttpRequestMessageBuilder WithAuthorization(this IAndHttpRequestMessageBuilder builder) => builder.WithHeader("X-Testing", "true");
    }
}