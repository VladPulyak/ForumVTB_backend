using BusinessLayer.Extensions;
using DataAccessLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDependencies(builder.Configuration.GetConnectionString("DefaultConnection"));
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
    options.AddPolicy("AllowAll", allow =>
    {
        allow.AllowAnyHeader();
        allow.AllowAnyMethod();
        allow.AllowAnyOrigin();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // JwtBearerDefaults.AuthenticationScheme
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
        googleOptions.ClientId = "235213662998-9eqe351jifk2urdcj1q6k38hfru1bcme.apps.googleusercontent.com";
        googleOptions.ClientSecret = "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";
        googleOptions.Scope.Add("email");
        googleOptions.Scope.Add("profile");
        googleOptions.Scope.Add("openId");
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await BusinessLayerStartupFunctions.MigrateDatabase(app.Services);

app.Run();


public static class BusinessLayerStartupFunctions
{
    public static async Task MigrateDatabase(IServiceProvider service)
    {
        using (var scope = service.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ForumVTBDbContext>();
            dbContext.Database.Migrate();
            await SeedDataAsync(dbContext);
        }
    }

    private static async Task SeedDataAsync(ForumVTBDbContext dbContext)
    {
        if (!await dbContext.Roles.AnyAsync())
        {
            await dbContext.Roles.AddRangeAsync(new List<IdentityRole>
            {
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "User", NormalizedName = "USER" }
            });

            await dbContext.SaveChangesAsync();
        }
    }
}