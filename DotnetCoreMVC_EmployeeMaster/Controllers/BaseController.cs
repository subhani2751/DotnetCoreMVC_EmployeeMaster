using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotnetCoreMVC_EmployeeMaster.Controllers
{
   [CustomeAuthfilter]
    public class BaseController : Controller
    {
        
    }
    public class CustomeAuthfilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
        }
    }
}
