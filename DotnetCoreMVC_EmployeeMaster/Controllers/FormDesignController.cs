using DotnetCoreMVC_EmployeeMaster.Externalfiles;
using DSInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreMVC_EmployeeMaster.Controllers
{
    //[Route("FormDesign")]
    public class FormDesignController : BaseController
    {
        //[HttpPost("PostData")]
        //public IActionResult PostData(FormBase FormData)
        public IActionResult PostData([ModelBinder(BinderType = typeof(CustomeModelbindeing))] FormBase model)
        {
            return RedirectToAction("Getdata", "FormDesign");
        }
        //[HttpGet("Getdata")]
        public IActionResult Getdata()
        {
            //return View("Employee"); // the view foldername and the controller name is same then its works like view is in Views/FormDesign/Employee.cshtml an controller name FormDesignController 
            //return View("Views/Employee/Employee.cshtml");
            return View("../Employee/Employee");// it checks only employee folder in the views folder
        }
    }
}
