using MudBlazor.Services;
using Pixsale.Services;
using Pixsale.Shared.Clients;
using Pixsale.Shared.Services;
using Pixsale.Web.Components;
using Pixsale.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add device-specific services used by the Pixsale.Shared project
builder.Services.AddSingleton<IDeviceInfoProvider, DeviceInfoProvider>();
builder.Services.AddSingleton<IDeviceInfoService, DeviceInfoService>();
builder.Services.AddApiClient();
builder.Services.AddMudServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(Pixsale.Shared._Imports).Assembly);

app.Run();
