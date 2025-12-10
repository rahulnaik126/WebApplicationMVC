using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Configuration.AddAzureAppConfiguration(
    options =>
    {
        options.Connect("Endpoint=https://webapplicationconfig.azconfig.io;Id=8rs5;Secret=F99YSJHv8ELjdPjZWxjMPrDQJ2SK0DGqVWrVKIFN4fdaKjOAPeNBJQQJ99BLACmepeScsAOxAAABAZAC3xvi");
        options.UseFeatureFlags();
    }
);

builder.Services.AddFeatureManagement();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
