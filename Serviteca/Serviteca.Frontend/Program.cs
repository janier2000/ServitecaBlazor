using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Serviteca.Frontend;
using Serviteca.Frontend.Repositories;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//cambio temporal
builder.Services
       .AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7057/") });

builder.Services.AddScoped<IRepository, Repository>();

// este es para  e nuge del idioma [Microsoft.Extensions.Localization]
builder.Services.AddLocalization();

// este es para  el nuge de mensaje  [CurrieTechnologies.Razor.SweetAlert2]
builder.Services.AddSweetAlert2();

//este es para  el nuge  [MudBlazor]
builder.Services.AddMudServices();

//builder.Services.AddMudServices(config =>
//{
//    config.PopoverOptions.ThrowOnDuplicateProvider = false;
//});

await builder.Build().RunAsync();