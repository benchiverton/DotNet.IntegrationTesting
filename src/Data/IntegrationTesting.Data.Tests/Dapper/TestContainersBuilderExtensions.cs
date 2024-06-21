using System;
using DotNet.Testcontainers.Builders;
using Testcontainers.MsSql;

namespace IntegrationTesting.Data.Tests.Dapper
{
    public static class TestContainersBuilderExtensions
    {
        public static MsSqlBuilder AddDockerEndpoint(this MsSqlBuilder testContainerBuilder)
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
