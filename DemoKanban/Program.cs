using DemoKanban.Infrastructure;
using DemoKanban.Middlewares;
using DemoKanban.Models;
using DemoKanban.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(o => o.ResourcesPath = "Resources");

// Add services to the container.
builder.Services.AddControllersWithViews();
//.AddMvcLocalization(); - depricated

//builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IEmailService, EmailService>();
//builder.Services.AddScoped<EmailService>();
//builder.Services.AddScoped<IEmailService, FakeEmailService>();
//builder.Services.AddScoped<IEmailService, EmailService>(sp => new EmailService(sp.GetService<IStringLocalizer>()));
//builder.Services.AddSingleton<IEmailService, EmailService>();

builder.Services.AddDbContext<KanbanContext>(o =>
    o.UseLazyLoadingProxies()
    .UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(
    //o => o.SignIn.RequireConfirmedAccount
    )
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<KanbanContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Ustawienia hasła:
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Ustawienia blokowania kont:
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // Ustawienia kont:
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Ustawienia ciasteczek:
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});



builder.Services.AddRazorPages();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseGlobalization();

app.MapRazorPages();

app.Map("/api/minApiIssue", (IEmailService emailService, KanbanContext ctx) =>
{
    return ctx.Issues;
});

app.MapIssueEnpints();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

//app.MapControllerRoute(
//    name: "default",
//    pattern: "/administration/{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Issue}/{action=Index}/{id?}");

app.MapControllerRoute(name: "blog_section",
    pattern: "blog/{*topic}",
    defaults: new { controller = "Blog", action = "Index" });

using(var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var kanbanContext = serviceScope.ServiceProvider.GetRequiredService<KanbanContext>();
    //kanbanContext.Database.Migrate();
    kanbanContext.Database.EnsureCreated();

    var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
    if(roleManager != null )
    {
        await CreateDefaultRoles.CreateRoles(roleManager);
    }
}

app.Run();