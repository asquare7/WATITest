using CodeTest02.Data;
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

            var summaries = new[]
            {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

            app.MapGet("/Hello", async () => "Hello World");

            //app.MapGet("/Add", async (int num1, int num2) =>
            //{
            //    int result = num1 + num2;
            //    //var addActivity = new Activity();
            //    //addActivity.Num1 = num1;
            //    //addActivity.Num2 = num2;
            //    //addActivity.Result = result;

            //    //context.Activities.Add(addActivity);
            //    //context.SaveChanges();

            //    return result;
            //});

            app.MapGet("/Add", async (int num1, int num2, AppDbContext context) =>
            {
                int result = num1 + num2;
                var addActivity = new Activity();
                addActivity.Num1 = num1;
                addActivity.Num2 = num2;
                addActivity.Result = result;

                context.Activities.Add(addActivity);
                context.SaveChanges();

                return result;
            });

            app.Run();
        }
    }
}