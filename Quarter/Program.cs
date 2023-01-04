using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quarter.DAL;
using Quarter.Models;
using Quarter.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<QuarterContext>(opt =>
{
    opt.UseSqlServer(@"SERVER=SAFAROFF\SQLEXPRESS01;Database=Quarter;Trusted_Connection=TRUE");
});

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 8;
    opt.Password.RequireNonAlphanumeric = false;
}).AddDefaultTokenProviders().AddEntityFrameworkStores<QuarterContext>();

builder.Services.AddScoped<LayoutService>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToAccessDenied = options.Events.OnRedirectToLogin = context =>
    {
        if (context.HttpContext.Request.Path.Value.StartsWith("/Admin"))
        {
            var redirectPath = new Uri(context.RedirectUri);
            context.Response.Redirect("/Admin/account/login" + redirectPath.Query);
        }
        else
        {
            var redirectPath = new Uri(context.RedirectUri);
            context.Response.Redirect("/account/login" + redirectPath.Query);
        }

        return Task.CompletedTask;
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
       name: "areas",
       pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
   );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
