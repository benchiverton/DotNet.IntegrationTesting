using System;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace IntegrationTesting.Data.Tests.TestInfrastructure
{
    public static class TestContainersBuilderExtensions
    {
        public static ITestcontainersBuilder<MsSqlTestcontainer> AddDockerEndpoint(this ITestcontainersBuilder<MsSqlTestcontainer> testContainerBuilder)
        {
            var dockerHost = Environment.GetEnvironmentVariable("DOCKER_HOST");
            if (!string.IsNullOrEmpty(dockerHost))
            {
                testContainerBuilder.WithDockerEndpoint(dockerHost);
            }
            return testContainerBuilder;
        }
    }
}
