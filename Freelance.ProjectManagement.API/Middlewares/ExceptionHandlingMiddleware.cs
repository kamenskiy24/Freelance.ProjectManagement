using FluentValidation;
using Freelance.ProjectManagement.Application.Exceptions;
using System.Net;
using ApplicationException = Freelance.ProjectManagement.Application.Exceptions.ApplicationException;

namespace Freelance.ProjectManagement.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string result;

            switch (exception)
            {
                case NotFoundException notFoundException:
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    result = notFoundException.Message;
                    break;
                case ValidationException:
                case ApplicationException:
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    result = exception.Message;
                    break;
                default:
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    result = exception.GetType() + "\n" + exception.Message + "\n" + exception.StackTrace;
                    break;
            }

            return context.Response.WriteAsync(result);
        }
    }
}

