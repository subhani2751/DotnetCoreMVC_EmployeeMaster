using DotnetCoreMVC_EmployeeMaster.Models;
using DSInterfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Reflection;

namespace DotnetCoreMVC_EmployeeMaster.Externalfiles
{
    public class FormBaseModelBinder : IModelBinder
    {
        private readonly IModelMetadataProvider _metadataProvider;

        public FormBaseModelBinder(IModelMetadataProvider metadataProvider)
        {
            _metadataProvider = metadataProvider;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var screenIdResult = bindingContext.ValueProvider.GetValue("Screenid");

            if (screenIdResult == ValueProviderResult.None)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            string screenId = screenIdResult.FirstValue;
            Type targetType = null;

            switch (screenId)
            {
                case "3":
                    targetType = typeof(EmployeeModel);
                    break;
                default:
                    bindingContext.Result = ModelBindingResult.Failed();
                    return;
            }

            var model = Activator.CreateInstance(targetType);
            ((FormBase)model).Screenid = Convert.ToInt32(screenId);

            // Bind remaining properties of derived type
            var modelBinderFactory = (IModelBinderFactory)bindingContext.HttpContext.RequestServices.GetService(typeof(IModelBinderFactory));
            var binder = modelBinderFactory.CreateBinder(new ModelBinderFactoryContext
            {
                BindingInfo = null,
                Metadata = _metadataProvider.GetMetadataForType(targetType),
                CacheToken = targetType
            });

            var newBindingContext = DefaultModelBindingContext.CreateBindingContext(
                bindingContext.ActionContext,
                bindingContext.ValueProvider,
                _metadataProvider.GetMetadataForType(targetType),
                bindingInfo: null,
                modelName: bindingContext.ModelName
            );

            await binder.BindModelAsync(newBindingContext);
            bindingContext.Result = ModelBindingResult.Success(newBindingContext.Model);
        }
    }

}