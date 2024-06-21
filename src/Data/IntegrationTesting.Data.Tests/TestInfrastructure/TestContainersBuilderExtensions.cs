using System;
using DotNet.Testcontainers.Builders;

namespace IntegrationTesting.Data.Tests.TestInfrastructure
{
    public static class TestContainersBuilderExtensions
    {
        public static ContainerBuilder AddDockerEndpoint(this ContainerBuilder testContainerBuilder)
        {
            var dockerHost = Environment.GetEnvironmentVariable("tcp://localhost:2375/");
            if (!string.IsNullOrEmpty(dockerHost))
            {
                testContainerBuilder.WithDockerEndpoint(dockerHost);
            }
            return testContainerBuilder;
        }
    }
}
