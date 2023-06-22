using CodeTest02.Data;
using CodeTest02.Dtos;
using CodeTest02.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeTest02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapPost("/Add", async (NumberDto numbers, AppDbContext context) =>
            {
                int result = numbers.Num1 + numbers.Num2;
                var addActivity = new Activity();
                addActivity.Num1 = numbers.Num1;
                addActivity.Num2 = numbers.Num2;
                addActivity.Result = result;

                context.Activities.Add(addActivity);
                context.SaveChanges();

                return result;
            });

            app.Run();
        }
    }
}