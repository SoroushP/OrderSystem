using Domain.Dto;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using WebAPI.ViewModel;

namespace WebAPI.ExceptionHandler
{
  public class GlobalExceptionHandler : IExceptionHandler
  {
    public Task HandleAsync(
        ExceptionHandlerContext context,
        CancellationToken cancellationToken)
    {
      var exception = context.ExceptionContext.Exception;

      HttpStatusCode statusCode;
      string code;
      string message;

      if (exception is BusinessException businessException)
      {
        statusCode = HttpStatusCode.BadRequest;
        code = businessException.Code;
        message = businessException.Message;
      }
      else
      {
        statusCode = HttpStatusCode.InternalServerError;
        code = "INTERNAL_ERROR";
        message = "An unexpected error occurred.";
      }

      var error = new ErrorViewModel
      {
        Code = code,
        Message = message
      };

      context.Result = new ResponseMessageResult(
          new HttpResponseMessage(statusCode)
          {
            Content = new StringContent(
                  JsonConvert.SerializeObject(error),
                  Encoding.UTF8,
                  "application/json")
          });

      return Task.CompletedTask;
    }
  }
}