using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat2.Models;

namespace WebProjekat2.Data
{
    public class StartData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<KarateContext>();
                context.Database.EnsureCreated();
            }
        }

        public static List<Test> GetTests()
        {
            List<Test> tests = new List<Test>()
            {
                new Test(){ Name="Test1"},
                new Test(){ Name="Test2"},
                new Test(){ Name="Test3"}
            };

            return tests;
        }
    }
}
