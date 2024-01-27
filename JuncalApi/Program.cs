using JuncalApi.DataBase;
using JuncalApi.Servicios;
using JuncalApi.Servicios.Excel;
using JuncalApi.Servicios.Facturar;
using JuncalApi.Servicios.Remito;
using JuncalApi.UnidadDeTrabajo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json.Serialization;
using System;
using Serilog;
using Serilog.Events;
using JuncalApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

// Serilog configuration

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error() // Establece el nivel mínimo a Error
    .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Error) // Muestra en consola desde Error
    .WriteTo.File(
        path: "Logs/log.txt",
        restrictedToMinimumLevel: LogEventLevel.Error, // Solo escribe en archivo desde Error
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();



// Register Serilog
builder.Host.UseSerilog();



builder.Services.AddDbContext<JuncalContext>(mysqlBuilder =>
{ 
    ;
});
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();
builder.Services.AddScoped<IServicioUsuario, ServicioUsuario>();
builder.Services.AddScoped<IServiceRemito,ServiceRemito>();
builder.Services.AddScoped<IServicioExcel, ServicioExcel>();
builder.Services.AddScoped<IFacturarServicio, FacturarServicio>();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddLogging();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddAutoMapper(typeof(Program).Assembly);


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCustomExceptionHandler();
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("corsapp");
app.MapControllers();
app.Run();
