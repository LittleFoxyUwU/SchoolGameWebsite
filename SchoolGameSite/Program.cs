using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SchoolGameSite.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Database") ?? "Data Source=Database.db";

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "SchoolGame API",
        Description = "API For my school game",
        Version = "v1"
    });
});

builder.Services.AddSqlite<DataBaseContext>(connectionString);


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(op =>
    {
        op.LoginPath = "/login";
        op.LogoutPath = "/logout";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else 
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "School Game API");
    });
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapGet("/api/", () => "Welcome to School Game API");
app.MapGet("/api/login_check", async (DataBaseContext db, HttpContext context) =>
{
    var user = await db.Users.FirstOrDefaultAsync(u => u.Username == context.User.Identity.Name);
    return user == null ? Results.Unauthorized() : Results.Ok(user);
});

app.Run();