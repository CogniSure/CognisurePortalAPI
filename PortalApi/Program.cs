using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Portal.Repository.Dashboard;
using Portal.Repository.Inbox;
using Portal.Repository.Login;
using Services.MsSqlServices.Interface;
using Services.Repository;
using Services.Repository.Interface;
using SqlServices;
using MsSqlAdapter;
using MsSqlAdapter.Interface;
using Services.Common.Interface;
using Common;
using Extention;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MsSqlServices;
using ApiServices;
using ApiServices.Interface;
using Global.Errorhandling;
using Repository;
using Microsoft.Extensions.Caching.Memory;
using Throttle.Filter;
using Microsoft.Extensions.Options;
using Services.Factory.Interface;
using Custom.Filter;
using PortalApi.HubConfig;
using AuthenticationHelper;
using SnowFlakeAdapter.Interface;
using SnowFlakeAdapter;
using Services.SnowFlakeServices.Interface;
using SnowFlakeServices;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .WithOrigins("http://localhost:4200")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});
SetupApplicationDependencies(builder.Services);
// Add services to the container.

//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<ThrottleFilter>();
//});
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
void SetupApplicationDependencies(IServiceCollection services)
{
    //services.AddScoped<IAPIConfiguration, APIConfiguration>();
    //services.AddTransient<HttpClient>();
    // HTTP Context Accessor
    services.AddHttpContextAccessor();
    services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();


    new SqlDIConfiguration().Setup(services);
    new SnowFlakeDIConfiguration().Setup(services);
    new ApiDIConfiguration().Setup(services);
    //new CS.CoreAPI.Adapters.Mongo.Services.DIConfiguration().Setup(services);

    builder.Services.AddTransient<IBusServiceFactoryResolver>(serviceProvider => key =>
    {
        switch (key)
        {
            case "mssql":
                {
                    var configure = new SqlDIConfiguration();
                    return configure.CreateIBusServiceFactory(serviceProvider);
                }
            case "sfdb":
                {
                    var configure = new SnowFlakeDIConfiguration();
                    return configure.CreateIBusServiceFactory(serviceProvider);
                }
            case "api":
                {
                    var configure = new ApiDIConfiguration();
                    return configure.CreateIBusServiceFactory(serviceProvider);
                }

            default:
                {
                    var configure = new SqlDIConfiguration();
                    return configure.CreateIBusServiceFactory(serviceProvider);
                }
        }
    });
    //services.AddSingleton<IMapperProvider<WebServiceMapperProfile>, MapperProvider<WebServiceMapperProfile>>();
    builder.Services.AddScoped<IDashboardService, DashboardService>();
    builder.Services.AddScoped<CustomFilterAttribute>();
    builder.Services.AddScoped<ThrottleFilter>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<ISubmissionService, SubmissionService>();
    builder.Services.AddScoped<IURLService, URLService>();
    builder.Services.AddScoped<IMsSqlDataHelper, MsSqlDataHelper>();
    builder.Services.AddScoped<ISnowFlakeDataHelper, SnowFlakeDataHelper>();
    builder.Services.AddScoped<IApiHelper, ApiHelper>();
    builder.Services.AddScoped<IMsSqlDatabase, MsSqlDatabase>();
    builder.Services.AddScoped<ISnowFlakeBaseDatabase, SnowFlakeBaseDatabase>();
    builder.Services.AddScoped<ISnowFlakeDatabase, SnowFlakeDatabase>();
    builder.Services.AddScoped<IExceptionService, ExceptionService>();
    builder.Services.AddScoped<IMsSqlBaseDatabase, MsSqlBaseDatabase>();
    builder.Services.AddScoped<IMsSqlDatabaseException, MsSqlDatabaseException>();
    builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
    builder.Services.AddScoped<IMsSqlDatabaseConfiguration, MsSqlDatabaseConfiguration>();
    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddSingleton<TimerManager>();
    builder.Services.AddScoped<IIpAddressServices, IpAddressServices>();
    builder.Services.AddScoped<BaseAuthenticationFactory, BaseAuthenticationFactory>();
    builder.Services.AddScoped<ICacheService, CacheService>();
    builder.Services.AddMemoryCache();
    string ChatbotSection = builder.Configuration.GetValue<string>("ChatbotAPI");
    builder.Services.AddHttpClient("chatclient", client =>
    {
        client.BaseAddress = new Uri(ChatbotSection);
    });
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "CogniSure Portal API", Version = "v1" });
        //c.EnableAnnotations();
        //c.CustomSchemaIds(i => i.FullName);

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
    });

    services.AddCors(p => p.AddPolicy("corspolicy", build =>
    {
        build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    }));

}
var app = builder.Build();
app.UseHttpStatusCodeExceptionMiddleware();
//app.UseThrottleMiddleware();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/notify");
app.Run();
