using MySqlConnector;
using slnLeagueTeamMap.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddTransient(x =>
// new MySqlConnection(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddSingleton<Services>();

var app = builder.Build();


// Configure the HTTP requesInvalidOperationException: 'Cannot modify ServiceCollection after application is built.'t pipeline.
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
