using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstralTest.Extensions
{
    public class ErrorFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var result = new
            {
                IsError = true,
                context.Exception.Message
            };
            context.HttpContext.Response.StatusCode = 500;
            context.Result = new JsonResult(result);
        }
    }
}
