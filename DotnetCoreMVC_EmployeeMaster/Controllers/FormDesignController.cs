using DotnetCoreMVC_EmployeeMaster.Externalfiles;
using DSInterfaces;
using DSInterfaces.Service;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreMVC_EmployeeMaster.Controllers
{
    //[Route("FormDesign")]
    public class FormDesignController : BaseController
    {
        //[Route("PostData")]
        //[HttpPost]
        //public IActionResult PostData(FormBase FormData)
        public IActionResult PostData([ModelBinder(BinderType = typeof(FormBaseModelBinder))] FormBase model)
        {
            return RedirectToAction("Getdata", "FormDesign");
        }
        //[Route("Getdata")]
        //[HttpGet]
        public IActionResult Getdata()
        {
            var a = FactoryInstance.GetObject<IEmployeeService>("EmployeeService");
            //return View("Employee"); // the view foldername and the controller name is same then its works like view is in Views/FormDesign/Employee.cshtml an controller name FormDesignController 
            //return View("Views/Employee/Employee.cshtml");
            return View("../Employee/Employee");// it checks only employee folder in the views folder
        }
    }
}
