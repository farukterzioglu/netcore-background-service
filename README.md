Related Article (TR) : https://medium.com/@farukterzioglu/net-core-i%CC%87le-arkaplan-servisler-572b5f7f5771  
İlgili makate : https://medium.com/@farukterzioglu/net-core-i%CC%87le-arkaplan-servisler-572b5f7f5771  

1. Create a console application
2. Add requirements
3. Setup Program.cs
4. Create generic host
5. Create & register background services
```
dotnet new console -n ConsoleApp
cd ConsoleApp
dotnet add package Microsoft.Extensions.Hosting
```

6. Register dependencies and use them
7. Configure options 
```
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.Options.ConfigurationExtensions
```

8. Configure multiple configurations
```
dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables
```

9. Configure logging
```
dotnet add package Microsoft.Extensions.Logging
dotnet add package Microsoft.Extensions.Logging.Configuration
dotnet add package Microsoft.Extensions.Logging.Console
```

10. Add serilog
```
dotnet add package Serilog.Extensions.Hosting
dotnet add package Serilog.Sinks.Console
dotnet add package Serilog.Settings.Configuration
```
11. Add collection config
