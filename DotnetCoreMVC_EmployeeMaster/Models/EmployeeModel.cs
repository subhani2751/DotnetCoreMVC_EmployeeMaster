using DSInterfaces;

namespace DotnetCoreMVC_EmployeeMaster.Models
{
    public class EmployeeModel : FormBase
    {
        public int? IMasterID { get; set; }
        public string  SName { get; set; }
        public  string? ProjectName { get; set; }
        public  string? CompanyName { get; set; }
       
    }
}
