using CodeTest02;
using CodeTest02.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace UnitTest
{
    public class UnitTest1
    {
        //AppDbContext db;

        [Fact]
        public async Task Test1()
        {
            //var options = new DbContextOptionsBuilder<AppDbContext>()
            //    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            //    .Options;

            //db = new AppDbContext(options);
            int num1 = 2, num2 = 4;

            var app = new WebApplicationFactory<Program>();

            using var client = app.CreateClient();

            var response = await client.GetStringAsync("/Add");

            NUnit.Framework.Assert.AreEqual(num1, num2, response);
        }
    }
}