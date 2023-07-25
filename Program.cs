using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ASP_CORE_MVC.Data;
using ASP_CORE_MVC.Models;

var builder = WebApplication.CreateBuilder(args);
/*Dependency Injection*/
builder.Services.AddDbContext<ASP_CORE_MVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ASP_CORE_MVCContext") ?? throw new InvalidOperationException("Connection string 'ASP_CORE_MVCContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

//이거 왜 없노 
   


var app = builder.Build();

/*Seed the Database*/
using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment()) {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

/*routing formmat*/
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//https://localhost:7157/Movies/Index/h
app.Run();
