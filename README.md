## Description

Base API

## Support

Kevelyn Nascimento 
<a href="https://www.linkedin.com/in/kevelynnascimento" target="_blank"><img src="https://img.shields.io/badge/LinkedIn-%230077B5.svg?&style=flat-square&logo=linkedin&logoColor=white" alt="LinkedIn"></a>

### Running the application

```
$ dotnet run --project src/GraphQL

$ dotnet watch --project src/GraphQL

$ dotnet run -- schema export --output schema.graphql
```

### Running tests

For the first execution, you need to install:
```
$ dotnet tool install -g dotnet-reportgenerator-globaltool
```

To execute unit tests:
```
$ dotnet test
```

To generate coverage test report on windows:
```
$ test_report.bat
```

### .NET Boilerplate

Hot chocolate - Used to build our GrapQL
.NET - v7 
C# v11
Debug using VS Code - Avoid the license
mongoDB
Repositories
Services
IoC
DevOps - Dennis




dotnet tool install -g dotnet-reportgenerator-globaltool