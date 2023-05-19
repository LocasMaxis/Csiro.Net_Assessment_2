using Csiro.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

IConfigurationRoot configuration;



configuration = new ConfigurationBuilder().AddJsonFile("./config.json").Build();

builder.Services.AddDbContext<ApplicantDataContext>(options =>
{
    var connectionString = configuration.GetConnectionString("DBConnection");
    options.UseSqlServer(connectionString);


});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
	        options.SignIn.RequireConfirmedEmail = true;
         })
				.AddEntityFrameworkStores<ApplicantDataContext>()    
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>();



builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
var app = builder.Build();








if (configuration["EnableDeveloperExceptions"]=="True")
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

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Register}/{id?}");
  


app.UseFileServer();

app.Run();

