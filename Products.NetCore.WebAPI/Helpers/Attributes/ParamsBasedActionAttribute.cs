using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using Products.NetCore.WebAPI.Helpers.Extensions;

namespace Products.NetCore.WebAPI.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ParamsBasedActionAttribute: ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            return action.Parameters.All(parameter => routeContext.IsQueryParameterPresent(parameter.Name));
        }
    }
}
