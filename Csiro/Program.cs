using Csiro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using NLog;
using NLog.Web;
using NLog.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();



var logger = NLog.LogManager.Setup().LoadConfigurationFromFile("./nlog.config").GetCurrentClassLogger();



IConfigurationRoot configuration;
configuration = new ConfigurationBuilder()
                 .AddJsonFile("./config.json")
    .Build();

configuration = new ConfigurationBuilder().AddJsonFile("./config.json").Build();

builder.Services.AddDbContext<ApplicantDbContext>(options =>
{
    var connectionString = configuration.GetConnectionString("DBConnection");
    options.UseSqlServer(connectionString);


});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
})
                .AddEntityFrameworkStores<ApplicantDbContext>()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>();



builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opts =>
{
    opts.Cookie.IsEssential = true; // make the session cookie Essential
});
var app = builder.Build();








if (configuration["EnableDeveloperExceptions"] == "True")
//if (env.IsDevelopment())
{
    app.UseExceptionHandler("/error.html");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// Configure the HTTP request pipeline.
else
{
    app.UseDeveloperExceptionPage();

}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


//app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");



app.UseFileServer();

app.Run();