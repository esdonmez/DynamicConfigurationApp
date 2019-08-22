# DynamicConfigurationApp
DynamicConfigurationApp is mainly a library which allows users to create a config with dynamic values. For now, the values are stored in MongoDb and Redis.
The library can be added to a project using dll's or nuget.

There are three parameters are needed to be able to call the library which are AppName, ConnectionString, and RefreshTimerInterval.
```sh
new ConfigurationReader(applicationName,connectionString,refreshTimerIntervalInMs);
```
The DynamicConfigurationApp developed using .NetCore2.0 and consists of three different projects;
- ClassLibrary
- XUnitTest
- WebPage

Used Nuget Packages are;
- StackExchange.Redis,
- MongoDB.Driver
- Autofac
- AutoMapper
- Newtonsoft.Json

The web application can be run using docker-compose. To be able to do that, you can write below commands while you are on the project directory.

```sh
$ docker-compose build
$ docker-compose up
```

### Problems
- Because of redis, the project is not working.
- There is a need to write more test cases.
- Need to test the project completely.


