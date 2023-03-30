using KanbanBlazorClient;
using KanbanBlazorClient.Auth;
using KanbanBlazorClient.Services;
using KanbanBlazorClient.Store;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var http = new HttpClient { BaseAddress = builder.HostEnvironment.IsDevelopment() ?
        new Uri("https://localhost:7108") :
        new Uri(builder.HostEnvironment.BaseAddress)};

builder.Services.AddScoped(sp => http);

builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IKanbanDemoService, KanbanDemoService>();

builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetService<CustomAuthenticationStateProvider>());

builder.Services.AddSingleton<FilterStore>();

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
