using System;
using System.Web.Http.Filters;

namespace JNCC.PublicWebsite.Core.Attributes.WebApi
{
    /// <summary>
    ///  A simple ActionFilterAttribute which add the "access-control-allow-origin" header to a given Action or Controller.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    internal sealed class SimpleEnableCorsAttribute : ActionFilterAttribute
    {
        private const string WildcardPolicy = "*";
        public string Policy { get; private set; }

        public SimpleEnableCorsAttribute(string policy = WildcardPolicy)
        {
            Policy = policy;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response.Headers.Add("access-control-allow-origin", Policy);
        }
    }
}
