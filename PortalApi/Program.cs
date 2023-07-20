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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IInboxRepository, InboxRepository>();
builder.Services.AddScoped<IURLRepository, URLRepository>();
builder.Services.AddScoped<IMsSqlDataHelper, MsSqlDataHelper>();
builder.Services.AddScoped<IMsSqlDatabase, MsSqlDatabase>();
builder.Services.AddScoped<IMsSqlBaseDatabase, MsSqlBaseDatabase>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddSingleton<SimpleCache>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build => {
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseCors("corspolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
