using DashboardRabitaBank.Services;
using DashboardRabitaBank.Settings;
using RabitaBank.Dashboard.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure MongoSettings
builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));

// Register InsightService
builder.Services.AddScoped<InsightService>();

builder.Services.Configure<GoogleMongoSettings>(builder.Configuration.GetSection("GoogleMongoSettings"));
builder.Services.AddScoped<GoogleReviewService>();

builder.Services.Configure<AppMongoSettings>(builder.Configuration.GetSection("AppMongoSettings"));
builder.Services.AddScoped<AppReviewService>();

builder.Services.Configure<FacebookMongoSettings>(builder.Configuration.GetSection("FacebookMongoSettings"));
builder.Services.AddScoped<FacebookService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
