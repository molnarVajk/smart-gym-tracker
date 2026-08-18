using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmartGymTracker.Api.Config;
using SmartGymTracker.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Controller-ek regisztrálása
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. DbContext regisztrálása (SQL Server használatával)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// * MEGJEGYZÉS: Ha In-Memory adatbázist szeretnél használni tesztelésre Migration nélkül:
// builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("GymTrackerDb"));

// 3. Egyéni szervizek regisztrálása (Dependency Injection)
builder.Services.AddScoped<TokenService>();

// 4. JWT Authentication konfigurációja
var jwtSecretKey = builder.Configuration["Jwt:Key"] ?? "SuperSecretKeyForSmartGymTracker12345!";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "SmartGymTracker",
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"] ?? "SmartGymTrackerUsers",
            ValidateLifetime = true
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 5. Autentikáció és Autorizáció használata
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
