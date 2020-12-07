### Serilog

Logging done using [Serilog Sink File](https://github.com/serilog/serilog-sinks-file) package.
All logs saved to the `/Logs` folder as configured in the application startup:

```
Log.Logger = new LoggerConfiguration()
  .Enrich.FromLogContext()
  .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
  .CreateLogger();

Log.Warning("Test log");
```