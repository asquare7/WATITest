using CodeTest02;
using CodeTest02.Data;
using CodeTest02.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using System;

namespace UnitTest
{
    public class UnitTest1 : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UnitTest1(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Test1()
        {
            using var client = _factory.CreateClient();

            var numbers = new NumberDto { Num1 = 5, Num2 = 7 };
            var content = new StringContent(JsonConvert.SerializeObject(numbers), System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync("/add", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = int.Parse(responseContent);

            // Verify the result
            Xunit.Assert.Equal(12, result);

            // Verify the database entry
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var activity = await dbContext.Activities.FirstOrDefaultAsync(a =>
                    a.Num1 == numbers.Num1 && a.Num2 == numbers.Num2);

                Xunit.Assert.NotNull(activity);
                Xunit.Assert.Equal(numbers.Num1, activity.Num1);
                Xunit.Assert.Equal(numbers.Num2, activity.Num2);
                Xunit.Assert.Equal(result, activity.Result);
            }
        }
    }
}