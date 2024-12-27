using Torneo.App.Persistencia;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Torneo.App.Frontend.Areas.Identity.Data;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);
var connectionString = "Data Source=/app/Torneo.db";
builder.Services.AddDbContext<IdentityDataContext>(options => options.UseSqlite(connectionString));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IdentityDataContext>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IRepositorioMunicipio, RepositorioMunicipio>();
builder.Services.AddSingleton<IRepositorioDT, RepositorioDT>();
builder.Services.AddSingleton<IRepositorioEquipo, RepositorioEquipo>();
builder.Services.AddSingleton<IRepositorioPartido, RepositorioPartido>();
builder.Services.AddSingleton<IRepositorioPosicion, RepositorioPosicion>();
builder.Services.AddSingleton<IRepositorioJugador, RepositorioJugador>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

// Add health checks
builder.Services.AddHealthChecks()
    .AddSqlite(connectionString, name: "database", failureStatus: HealthStatus.Unhealthy, tags: new[] { "db" });

//Configure Data Protection to persist keys to a specific directory
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"/app/.aspnet/DataProtection-Keys"))
    .SetApplicationName("TournamentManagementApp");


/* Configurar para siguiente sprint
builder.Services.AddAuthentication()
   .AddGoogle(options =>
   {
       IConfigurationSection googleAuthNSection =
       config.GetSection("Authentication:Google");
       options.ClientId = googleAuthNSection["ClientId"];
       options.ClientSecret = googleAuthNSection["ClientSecret"];
   })
   .AddFacebook(options =>
   {
       IConfigurationSection FBAuthNSection =
       config.GetSection("Authentication:FB");
       options.ClientId = FBAuthNSection["ClientId"];
       options.ClientSecret = FBAuthNSection["ClientSecret"];
   })
   .AddMicrosoftAccount(microsoftOptions =>
   {
       microsoftOptions.ClientId = config["Authentication:Microsoft:ClientId"];
       microsoftOptions.ClientSecret = config["Authentication:Microsoft:ClientSecret"];
   });
   */


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapHealthChecks("/health");

app.Run();
