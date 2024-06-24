using System;
using DotNet.Testcontainers.Builders;
namespace IntegrationTesting.Data.Tests.TestContainers
{
    public static class TestContainersBuilderExtensions
    {
        public static ContainerBuilder AddDockerEndpoint(this ContainerBuilder testContainerBuilder)
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
