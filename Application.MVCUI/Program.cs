
using Application.Business.Concrete;
using Application.Business.Extensions;
using Application.Core.Configuration.Context;
using Application.Core.Configuration.Environment;
using Application.Core.Extensions;
using Application.Core.Utilities.DependencyServiceTool;
using Application.MVCUI.Services.Session;
using Application.Packages.HttpClient;
using Application.Packages.HttpClientService;
using Application.Packages.JWT.Entities;
using Application.Packages.JWT.Extensions;
using Application.Packages.RabbitMQ.Configuration;
using Application.Packages.RabbitMQ.Extensions;
using Application.Packages.RabbitMQ.Publisher;
using Application.Packages.RabbitMQ.Service;
using Autofac;
using Autofac.Extensions.DependencyInjection;

IApplicationConfigurationContext ConfigurationContext = new ApplicationConfigurationContext(new EnvironmentService());

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCoreModule();
builder.Services.AddSession();
// Register business module. ðŸŽ‰
builder.Services.AddBusinessModule(ConfigurationContext);

builder.Services.AddJWT(configuration =>
{
    configuration.SecretKey = ConfigurationContext.JWTKey;
    configuration.Issuer = ConfigurationContext.JWTIssuer;
    configuration.Audience = ConfigurationContext.JWTAudience;
    configuration.ExpiryHour = ConfigurationContext.JWTExpiryHour;
    configuration.TokenSecurityAlgorithms = EnumTokenSecurityAlgorithms.HmacSha256;
});

builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddSingleton<IHttpService, HttpService>();

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation(); 

builder.Services.AddRabbitMQ(configuration =>
{
    configuration.Host = ConfigurationContext.RabbitMQHost;
    configuration.Port = ConfigurationContext.RabbitMQPort;
    configuration.Username = ConfigurationContext.RabbitMQUsername;
    configuration.Password = ConfigurationContext.RabbitMQPassword;
});
builder.Services.AddSingleton<IHttpService, HttpService>();
builder.Services.AddSingleton<ISessionService, SessionService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

DependencyServiceTool.CreateServiceProvider(builder.Services);
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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();