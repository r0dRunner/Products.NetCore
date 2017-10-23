using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Products.NetCore.Service.Helpers.Exceptions;
using Products.NetCore.WebAPI.DTOs;

namespace Products.NetCore.WebAPI.Helpers.Filters
{
    public class ExceptionResultFilter: IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var error = new ErrorDTO
            {
                ErrorCode = context.Exception.GetType().Name,
                ErrorMessage = context.Exception.Message,
                StackTrace = context.Exception.StackTrace,
                Description = context.Exception.HelpLink
            };

            var result = new ObjectResult(error)
            {
                StatusCode = 500
            };

            if (context.Exception is NotFoundException)
            {
                result.StatusCode = 404;
            }
            
            context.Result = result;

            //Log ErrorDTO
        }
    }
}
