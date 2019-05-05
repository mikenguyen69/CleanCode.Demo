using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CleanCode.Demo.WebAPI.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // To-Do: add in validation for the context to secure the API
        }
    }
}