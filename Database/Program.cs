using CenterEnglishManagement.Context;
using CenterEnglishManagement.Extentions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
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
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "sadfsadfasdfsdfdsafdasdfasdf",
            ValidAudience = "asdfsadfsadfsdffdsfasdfasdfsa",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hfdghfghdfghfghdfghfgvbcnfsdghfnbavmsrfdsjkhfgsnmbdsvajhgxchzjbbvygvouiyafs"))
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("StudentPolicy", policy => policy.RequireRole(UserRole.Student.ToString()));
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole(UserRole.Admin.ToString()));
    options.AddPolicy("TeacherPolicy", policy => policy.RequireRole(UserRole.Teacher.ToString()));
    options.AddPolicy("StudentOrTeacherPolicy", policy =>
        policy.RequireRole(UserRole.Student.ToString(), UserRole.Teacher.ToString()));
});
// Add services to the container.
builder.Services.AddControllersWithViews();
//register applicationDBContext
String defaultContection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(defaultContection));
//
builder.Services.AddScopeService();
builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
