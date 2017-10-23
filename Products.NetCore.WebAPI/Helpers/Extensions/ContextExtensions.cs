using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;

namespace Products.NetCore.WebAPI.Helpers.Extensions
{
    public static class ActionContextExtensions
    {
        public static string GetRouteTemplate(this ActionContext context)
            => context.ActionDescriptor.AttributeRouteInfo.Template;

        public static string GetController(this ActionContext context) => GetFromRouteData(context, "controller");

        public static string GetAction(this ActionContext context) => GetFromRouteData(context, "action");

        private static string GetFromRouteData(ActionContext context, string key)
            => context.HttpContext.GetRouteData().Values[key].ToString();
    }

    public static class RouteContextExtensions
    {
        public static bool IsQueryParameterPresent(this RouteContext context, string parameterName)
            => GetFromRouteData(context, parameterName);

        private static bool GetFromRouteData(RouteContext context, string key)
            => context.HttpContext.Request.Query.TryGetValue(key, out var value);
    }
}
