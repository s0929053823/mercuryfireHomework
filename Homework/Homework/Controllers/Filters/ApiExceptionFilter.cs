
using Homework.Common;
using Homework.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoindeskHomework.Controllers.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly IMercuryfireHomeworkContextProcedures _sp;

        public ApiExceptionFilter(MercuryfireHomeworkContext db)
        {
            _sp = db.Procedures;
         
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var request = context.HttpContext.Request;


            var controllerName = context.ActionDescriptor.RouteValues["controller"];
            var actionName = context.ActionDescriptor.RouteValues["action"];
            var httpMethod = context.HttpContext.Request.Method;
            OutputParameter<string> rtnValue = null;
            _sp.usp_AddLogAsync(0,"sp_name",new Guid(), controllerName, actionName, rtnValue).GetAwaiter().GetResult();
     

            var errorResponse = new
            {
                message = "An unexpected error occurred.",
                error = exception.Message
            };

            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };


            if (context.Exception is NotFoundException notFoundException)
            {
                context.Result = new ObjectResult(new { message = notFoundException.Message })
                {
                    StatusCode = StatusCodes.Status404NotFound
                };
                context.ExceptionHandled = true;
                return;
            }


            context.ExceptionHandled = true;
        }
    }
}
