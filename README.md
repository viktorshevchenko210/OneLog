# OneLog [![NuGet Version](http://img.shields.io/nuget/v/OneLog.svg?style=flat)](https://www.nuget.org/packages/OneLog/)
OneLog is a logging library for ASP.NET Core applications, that writes structured events into file in JSON format. All events are written within an HTTP request will be written into file as one JSON object. 
## Getting started
OneLog is installed from NuGet.
```
Install-Package OneLog
```
#### Firstly, you need to set up configuration in your appsettings file. 
```
"OneLog": 
{
     "LoggerFolder": "C:\\Users\\Shevchenko\\source\\repos\\OneLog\\Logs\\",
     "FileName": "Requests",
     "IsBuffered": "true" 
}
```
Description of configuration:
 * LoggerFolder - folder, where files will be created
 * FileName - name for log file
 * IsBuffered - If IsBuffered is false, logs will be written to file at once, but it will slow down your application a little bit

#### Secondly, you add to ConfigureServices method:
```
public void ConfigureServices(IServiceCollection services)
{
     services.AddOneLog(Configuration);
}
```
#### Then, you add to Configure method:
```
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
     app.UseOneLog();
}
```
That is it for configuration and it is ready to use. 
## Usage
Use ILogger interface. It consists of two methods: LogEvent and LogException.
```
public interface ILogger
{
   Request Request { get; }
   void LogEvent(string name, string value, EventCategory category);
   void LogException(Exception ex);
}
logger.LogEvent("REQEUST", "START", EventCategory.Information);
```
## Getting help
To learn more about OneLog, check out the [documentation](https://github.com/viktorshevchenko210/OneLog/wiki).
