using System.Reflection;
using System.Web.Mvc;

namespace JNCC.PublicWebsite.Core.Attributes.Routing
{
    public sealed class RequireParameterAttribute : ActionMethodSelectorAttribute
    {
        public RequireParameterAttribute(string valueName)
        {
            ValueName = valueName;
        }
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return (controllerContext.HttpContext.Request[ValueName] != null);
        }
        public string ValueName { get; private set; }
    }
}
