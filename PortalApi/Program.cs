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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();
builder.Services.AddScoped<IInboxRepository, InboxRepository>();
builder.Services.AddScoped<IURLRepository, URLRepository>();
builder.Services.AddScoped<IMsSqlDataHelper, MsSqlDataHelper>();
builder.Services.AddSingleton<IMsSqlDatabase, MsSqlDatabase>();
builder.Services.AddSingleton<IMsSqlBaseDatabase, MsSqlBaseDatabase>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
