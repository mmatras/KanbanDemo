using KanbanBlazorClient;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var http = new HttpClient { BaseAddress = builder.HostEnvironment.IsDevelopment() ?
        new Uri("https://localhost:7108") :
        new Uri(builder.HostEnvironment.BaseAddress)};

builder.Services.AddScoped(sp => http);

await builder.Build().RunAsync();
