using Domain.Service;
using Serilog;
using System;

namespace Service.Logging
{
  public class SerilogAppLogger : IAppLogger
  {
    private readonly ILogger logger;

    public SerilogAppLogger(ILogger logger)
    {
      this.logger = logger;
    }

    public void Debug(string message, params object[] args)
    {
      logger.Debug(message, args);
    }

    public void Information(string message, params object[] args)
    {
      logger.Information(message, args);
    }

    public void Warning(string message, params object[] args)
    {
      logger.Warning(message, args);
    }

    public void Error(
        Exception exception,
        string message,
        params object[] args)
    {
      logger.Error(exception, message, args);
    }
  }
}
