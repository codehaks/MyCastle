using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyApp.Common;
using MyCastle.Models;
using System;
using System.Reflection;
using System.Security.Claims;

namespace MyCastle.Common
{
    public class UserInfoAttribute : Attribute, IPageFilter
    {
        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {

            //
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            //throw new NotImplementedException();
        }
    }
}