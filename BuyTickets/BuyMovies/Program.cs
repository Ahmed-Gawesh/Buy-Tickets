using BuyMovies.Cart;
using BuyMovies.Helpers;
using BuyMovies.MapperProfiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Movies.BLL.Interfaces;
using Movies.BLL.Repositories;
using Movies.DAL;
using Movies.DAL.Contexts;
using Movies.DAL.Models;
using Project.PL.Settings;
using StackExchange.Redis;
using System;

namespace BuyMovies
{
    public class Program
    {
        public static async Task Main(string[] args) 
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Services That Allow Dependency Injection
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<MovieContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });



            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            

            builder.Services.AddAutoMapper(A => A.AddProfile(new ActorProfile()));
            builder.Services.AddAutoMapper(A => A.AddProfile(new ProducerProfile()));
            builder.Services.AddAutoMapper(A => A.AddProfile(new CinemaProfile()));
            builder.Services.AddAutoMapper(A => A.AddProfile(new MovieProfile()));
            builder.Services.AddAutoMapper(A => A.AddProfile(new UserProfile()));


            builder.Services.AddAuthentication();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<MovieContext>();
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "Account/Login";
                        options.AccessDeniedPath = "Home/Error";
                    }); //For UserManager,SignInManager

			builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
			builder.Services.AddTransient<IEmailSettings, EmailSettings>();


            #region Connection Redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
               {
                   var connected = builder.Configuration.GetConnectionString("Redis");
                   return ConnectionMultiplexer.Connect(connected);
               }); 
            #endregion

            #endregion



            var app = builder.Build();

            #region Update DataBase

            var scope = app.Services.CreateScope();  //Create services Of Scope
            var services = scope.ServiceProvider;  // Generate DI OF DbContext
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = services.GetRequiredService<MovieContext>();
                await dbContext.Database.MigrateAsync(); //Update DataBase
                await MovieContextSeed.SeedAsync(dbContext); //For Seeding Data



            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occured During Apply Migration");

            }
            #endregion

          
            #region Configure the HTTP request pipeline.

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Movies}/{action=Index}/{id?}");

            #endregion

            app.Run();

            
        }
    }
}