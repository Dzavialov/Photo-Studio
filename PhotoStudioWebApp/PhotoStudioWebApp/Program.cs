using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhotoStudio.Application.DTOs;
using PhotoStudio.Application.Mappings;
using PhotoStudio.Application.Validation;
using PhotoStudio.Domain;
using PhotoStudio.Domain.Entities;
using System.Text;

namespace PhotoStudioWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigurationManager configuration = builder.Configuration;

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            builder.Services.AddFluentValidationClientsideAdapters();

            // For Entity Framework
            builder.Services.AddDbContext<AuthorizationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnStr")));

            //Adding validation dependencies
            builder.Services.AddScoped<IValidator<RoomDto>, RoomDtoValidation>();
            builder.Services.AddScoped<IValidator<BookingDto>, BookingDtoValidation>();
            builder.Services.AddScoped<IValidator<EquipmentItemDto>, EquipmentItemDtoValidation>();
            //builder.Services.AddScoped<IValidator<RegisterDto>, RegisterDtoValidation>();
            //builder.Services.AddScoped<IValidator<LoginDto>, LoginDtoValidation>();

            // For Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthorizationDbContext>()
                .AddDefaultTokenProviders();

            

            // Adding Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // Adding Jwt Bearer
              .AddJwtBearer(options =>
              {
                  options.SaveToken = true;
                  options.RequireHttpsMetadata = false;
                  options.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidAudience = configuration["JWT:ValidAudience"],
                      ValidIssuer = configuration["JWT:ValidIssuer"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                  };
              });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Authentication & Authorization
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}