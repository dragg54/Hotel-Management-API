using Hotel_Management_API.Data.DBContexts;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Hotel_Management_API.Services;
using Hotel_Management_API.Entities;
using Microsoft.AspNetCore.Identity;
using Hotel_Management_API.Bootstrapper.Configurations;
using Hotel_Management_API.Middlewares;
using Hotel_Management_API.Responses;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using FluentValidation.AspNetCore;
using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.DTOs.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<PostOwnerRequestValidator>();
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddIdentity<User, IdentityRole>()
       .AddEntityFrameworkStores<HotelDBContext>();
//jwt
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var jwtIssuer = builder.Configuration.GetSection("JwtSettings:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("JwtSettings:SecretKey").Get<string>();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
     options.Events = new JwtBearerEvents
     {
         OnChallenge = context =>
         {
             context.HandleResponse();
             context.Response.StatusCode = StatusCodes.Status401Unauthorized;
             return Task.CompletedTask;
         },
         OnAuthenticationFailed = context =>
         {
             Console.WriteLine($"Authentication failed: {context.Exception.Message}");
             return Task.CompletedTask;
         },
     };

 });
//end of jwt


builder.Services.AddScoped<RoleManager<IdentityRole>>();

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true);
builder.Configuration.AddEnvironmentVariables();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();


builder.Services.AddDbContext<HotelDBContext>(options =>
{
    Log.Information($"Using {builder.Environment.EnvironmentName} DB");
    var connectionString = builder.Configuration.GetConnectionString("DbConnection");
    if (builder.Environment.IsDevelopment())
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
    else
    {
        connectionString = builder.Configuration.GetConnectionString("SQLConnection");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
});

//Authorization
builder.Services.AddAuthorization(options =>
       {
           options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
       });

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

// builder.Services.AddHttpsRedirection(options =>
// {
//     options.HttpsPort = 5083; // Set your HTTPS port here
// });

//configure swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Hotel Management API",
        Version = "v1"
    });

    // Configure Swagger to use Bearer authentication
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\nExample: 'Bearer abc123xyz'",
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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

//Inject services    
builder.Services.AddScoped<IResponseHandler, ResponseHandler>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IOwnerService, OwnerService>();
builder.Services.AddScoped<IHotelService, HotelService>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseMiddleware<GlobalExceptionHandler>();

//if (builder.Environment.IsProduction())
//{
//    Log.Information("DI for context in production");
//    var db = app.Services.GetRequiredService<HotelDBContext>();
//    await db.Database.MigrateAsync();

//}



app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
