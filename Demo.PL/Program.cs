using AutoMapper;
using Demo.BL.AutoMapper;
using Demo.BL.Interface;
using Demo.BL.ModelVM;
using Demo.BL.Repository;
using Demo.BL.SignalR;
using Demo.DAL.DataBase;
using Demo.DAL.Extend;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json.Serialization;

namespace Demo.PL
{
    public class Program
    {
        public static Action<SqlServerDbContextOptionsBuilder>? DemoConnection { get; private set; }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            // Enhancement ConnectionString
            var connectionString = builder.Configuration.GetConnectionString("DemoConnection");

            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(connectionString));

            //Auto Mapper
            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

            //identity configyration
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
             options =>
             {
                 options.LoginPath = new PathString("/Account/Login");
                 options.AccessDeniedPath = new PathString("/Account/Login");
             });

            //starup 
            builder.Services.AddSignalR();

            builder.Services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>(TokenOptions.DefaultProvider);


            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // Default Password settings.
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            }).AddEntityFrameworkStores<ApplicationDBContext>();

            //add scoped 
            builder.Services.AddScoped<IDepartment, DepartmentRep>();
            builder.Services.AddScoped<IEmployee, EmployeeRep>();
            builder.Services.AddScoped<ICountry, CountryRep>();
            builder.Services.AddScoped<ICity, CityRep>();
            builder.Services.AddScoped<IDistrict, DistrictRep>();





            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.MapHub<ChatHub>("/Chat");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}