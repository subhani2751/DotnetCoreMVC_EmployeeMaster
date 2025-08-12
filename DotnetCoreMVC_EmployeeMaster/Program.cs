using DotnetCoreMVC_EmployeeMaster.Externalfiles;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

string xmlFilePath = Path.Combine(builder.Environment.ContentRootPath, "InstancesXML.xml");
FactoryInstance.LoadFromXml(xmlFilePath);
// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddControllersWithViews(options =>
//{
//    options.ModelBinderProviders.Insert(0, new FormBaseModelBinderprovider());
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FormDesign}/{action=Getdata}/{id?}");

app.Run();
