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
using PortalApi.FactoryResolver;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MsSqlServices;
using ApiServices;
using ApiServices.Interface;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
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
SetupApplicationDependencies(builder.Services);
// Add services to the container.

builder.Services.AddControllers();
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
    new ApiDIConfiguration().Setup(services);
    //new CS.CoreAPI.Adapters.Mongo.Services.DIConfiguration().Setup(services);

    services.AddTransient<IBusServiceFactoryResolver>(serviceProvider => key =>
    {
        switch (key)
        {
            case "mssql":
                {
                    var configure = new SqlDIConfiguration();
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
    builder.Services.AddScoped<IDashboardRepository, DashboardService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<ISubmissionService, SubmissionService>();
    builder.Services.AddScoped<IURLService, URLService>();
    builder.Services.AddScoped<IMsSqlDataHelper, MsSqlDataHelper>();
    builder.Services.AddScoped<IApiHelper, ApiHelper>();
    builder.Services.AddScoped<IMsSqlDatabase, MsSqlDatabase>();
    builder.Services.AddScoped<IMsSqlBaseDatabase, MsSqlBaseDatabase>();
    builder.Services.AddScoped<ITokenService, TokenService>();
    builder.Services.AddSingleton<SimpleCache>();

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

    services.AddCors(p => p.AddPolicy("corspolicy", build => {
        build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    }));

}

//builder.Services.AddCors(p => p.AddPolicy("corspolicy", build => {
//    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
//}));

var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();
app.UseCors("corspolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
