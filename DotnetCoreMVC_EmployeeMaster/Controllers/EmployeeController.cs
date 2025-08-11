using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreMVC_EmployeeMaster.Controllers
{
    public class EmployeeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
