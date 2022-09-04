# DotNet Integration Testing

Unit tests for code that has external dependencies in DotNet.

## Areas covered

| Area             | Details                                                        |
| ---------------- | -------------------------------------------------------------- |
| [Data](#data)    | Testing code which depends on SQL Server being available       |
| [API](#api)      | Testing API code with concrete implementations of dependencies |

### [Data](src/Data/)

[![Data - build and test](https://github.com/benchiverton/DotNet.IntegrationTesting/actions/workflows/ci_build-and-test-data.yml/badge.svg)](https://github.com/benchiverton/DotNet.IntegrationTesting/actions/workflows/ci_build-and-test-data.yml)

This test project shows how you can effectively test your database code, including stored procedures, login/user access, data truncation, and pretty much any SQL Server feature you can think of.

The interesting part of this test project is the [DatabaseContainer](src/Data/IntegrationTesting.Data.Tests/TestInfrastructure/DatabaseContainer.cs) class. This class:
1. Spins up a SQL Server container using [testcontainers-dotnet](https://github.com/testcontainers/testcontainers-dotnet),
2. Deploys the latest SQL Server dacpac (build from the latest [sqlproj](src/Data/IntegrationTesting.Data.Sql/)) to the SQL Server container,
3. Provides SQL connection strings for SQL logins that can be used to connect to the running SQL Server container.

Dependencies/downsides:
* You must be able to access a docker endpoint (remote or local).
* You must be able to complie sqlproj's (requires VS data build tools).
* Container creation + dacpac deployment takes a while (~30 seconds). The impact of this can be reduced by using [Collection Fixtures](src/Data/IntegrationTesting.Data.Tests/TestInfrastructure/DatabaseCollection.cs).

### [API](src/Api/)

TODO

## Running the tests

### CI/CD

The build pipeline uses GitHub actions. Any custom/non-public docker images used are built from the [docker](docker/) directory.

### Local

Dependencies for running tests locally are as follows:
* Access to a docker endpoint via one of the following
  * Docker Desktop
  * Access to a remote Docker endpoint (remember to set `DOCKER_HOST`!)
  * Docker installed locally [follow these instructions if you're using windows!](docs/installing_docker_windows.md) (remember to set `DOCKER_HOST`!)
* Visual Studio data build tools (to build .sqlproj's)
