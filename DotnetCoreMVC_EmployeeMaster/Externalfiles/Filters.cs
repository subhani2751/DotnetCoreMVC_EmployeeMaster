using DotnetCoreMVC_EmployeeMaster.Models;
using DSInterfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Reflection;

namespace DotnetCoreMVC_EmployeeMaster.Externalfiles
{
    public class CustomeModelbinderprovider : DefaultModelBindingContext, IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            //if (context.Metadata.ModelType == typeof(FormBase))
            //{
            //    return new CustomeModelbindeing();
            //}
            throw new NotImplementedException();
        }
    }
    public class CustomeModelbindeing :  IModelBinder
    {
        private readonly IModelMetadataProvider _metadataProvider;
        public CustomeModelbindeing(IModelMetadataProvider modelMetadataProvider)
        {
            _metadataProvider = modelMetadataProvider;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string? id = string.Empty;
            var iscreenid = bindingContext.ValueProvider.GetValue("Screenid");
            if (iscreenid == null)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }
            else{
                id = iscreenid.FirstValue;
            }

            if (!string.IsNullOrEmpty(id))
            {
                Type type = null;

                if (id == "3")
                {
                    type= typeof(EmployeeModel);
                    var obj=Activator.CreateInstance(type);
                    //((FormBase)obj).Screenid = Convert.ToInt32(id);
                    //bindingContext.Result=ModelBindingResult.Success(obj);
                    bindingContext.ModelMetadata = _metadataProvider.GetMetadataForType(type);
                    //bindingContext.ModelMetadata.ModelType = obj;
                }
            }
            return Task.CompletedTask;
        }

    }

    //public class CustomeModelbindeing1 : ComplexObjectModelBinder
    //{
        
    //}
}
