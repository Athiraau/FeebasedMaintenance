using Business.Contracts;
using Business.Helpers;
using Business.Services;
using DataAccess.Context;
using DataAccess.Dto.Request;
using DataAccess.Dto;
using DataAccess.Entities;
using FluentValidation;
using MaintenanceAPI.Validators;
using NLog.Extensions.Logging;
using DataAccess.Repository;
using MaintenanceAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);


var logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
NLog.GlobalDiagnosticsContext.Set("LogDirectory", logPath);

builder.Logging.AddNLog(logPath).SetMinimumLevel(LogLevel.Trace);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<DapperContext>();
builder.Services.AddTransient<ILoggerService, LoggerService>();
builder.Services.AddTransient<IServiceWrapper, ServiceWrapper>();
builder.Services.AddTransient<HelperWrapper>();
builder.Services.AddTransient<IMaintenanceService, MaintenanceService>();
builder.Services.AddTransient<MaintainRepo>();
builder.Services.AddTransient<DtoWrapper>();
builder.Services.AddTransient<ErrorResponse>();
builder.Services.AddTransient<IJwtUtils, JwtUtils>();
builder.Services.AddTransient<IValidator<ReqDto>, CommonValidators>();

builder.Services.AddTransient<CommonValidationHelper>();


// Add Cors
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed((host) => true);
}));


builder.Services.Configure<IISOptions>(options =>
{
    options.AutomaticAuthentication = false;
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(x =>
    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage();

app.UseSwagger();
// This middleware serves the Swagger documentation UI
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Leave Data Module API V1");
});


app.UseRouting();

app.UseCors("MyPolicy");

app.UseMiddleware<CorsMiddleware>();
app.UseMiddleware<JwtMiddlewareExtension>();
app.UseMiddleware<ExceptionMiddleware>();
//app.UseHttpsRedirection();
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
