using Demo.BL.AutoMapper;
using Demo.BL.Interface;
using Demo.BL.Repository;
using Demo.DAL.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Demo.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Enhancement ConnectionString
            var connectionString = builder.Configuration.GetConnectionString("DemoConnection");

            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(connectionString));

            //Auto Mapper
            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));


            builder.Services.AddScoped<IEmployee, EmployeeRep>();

            builder.Services.AddCors();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(options => options
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}