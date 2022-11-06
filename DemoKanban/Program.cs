using DemoKanban.Middlewares;
using DemoKanban.Models;
using DemoKanban.Services;

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

app.UseGlobalization();

app.Map("/api/minApiIssue", (IEmailService emailService) =>
{
    return KanbanContext.Data.Issues;
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

app.Run();