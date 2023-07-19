using BusinessLayer.Extensions;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDependencies($"Host={builder.Configuration.GetSection("ConnectionString:Host").Value!};Port={builder.Configuration.GetSection("ConnectionString:Port").Value!};Database={builder.Configuration.GetSection("ConnectionString:Database").Value!};Username={builder.Configuration.GetSection("ConnectionString:Username").Value!};Password={builder.Configuration.GetSection("ConnectionString:Password").Value!}",
    $"Host={builder.Configuration.GetSection("VehiclesInfoConnectionString:Host").Value!};Port={builder.Configuration.GetSection("VehiclesInfoConnectionString:Port").Value!};Database={builder.Configuration.GetSection("VehiclesInfoConnectionString:Database").Value!};Username={builder.Configuration.GetSection("VehiclesInfoConnectionString:Username").Value!};Password={builder.Configuration.GetSection("VehiclesInfoConnectionString:Password").Value!}");

builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Forum-VTB project. Template: (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(allow =>
    {
        allow.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = "Forum-VTB",
        ValidAudience = "Forum-VTB",
        ClockSkew = TimeSpan.Zero
    };
})
.AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration.GetSection("Authentication:Google:ClientId").Value!;
    googleOptions.ClientSecret = builder.Configuration.GetSection("Authentication:Google:ClientSecret").Value!;
});

var app = builder.Build();

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features
        .Get<IExceptionHandlerPathFeature>()
        .Error;
    var response = new { error = exception.Message };
    await context.Response.WriteAsJsonAsync(response);
}));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await StartupServices.MigrateDatabase(app.Services);

app.Run();