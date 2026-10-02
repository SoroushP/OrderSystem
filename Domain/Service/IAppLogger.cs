using System;

namespace Domain.Service
{
  public interface IAppLogger
  {
    void Information(string message, params object[] args);
    void Warning(string message, params object[] args);
    void Error(Exception exception, string message, params object[] args);
  }
}
