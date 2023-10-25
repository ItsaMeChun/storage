using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using dotenv.net;
using dotenv.net.Utilities;
using hcode.Data;
using hcode.Entity;
using hcode.Repository;
using hcode.Repository.imp;
using hcode.Service;
using hcode.Service.imp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";   //CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyHeader()
                          .AllowAnyOrigin()
                          .AllowAnyMethod();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

DotEnv.Load();
// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseNpgsql(EnvReader.GetStringValue("CONNECTION_STRING"));
});

//var sercetKey = builder.Configuration.GetValue<string>("JWTSecretKey");
var secretKey = EnvReader.GetStringValue("JWTSecretKey");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("AuthorizeOnly", policy =>
    {
        policy.RequireClaim("UserId");
    });
});

builder.Services.AddScoped<IRepository<Author>, Repository<Author>>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
