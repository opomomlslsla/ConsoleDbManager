
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PTMK_TestTask.BuisnessLogic;
using PTMK_TestTask.Domain.Interfaces;
using PTMK_TestTask.Ifrastructure;
using PTMK_TestTask.Ifrastructure.Repository;

namespace PTMK_TestTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddDbContext<Context>(options =>
                    {
                        options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=TestDatabase; Trusted_Connection = true;");
                    })
                    .AddScoped<IEmployeeService,EmployeeService>()
                    .AddScoped<IEmployeeRepository, EmployeeRepository>()
;
                })
                .Build();

            var service = ActivatorUtilities.GetServiceOrCreateInstance<IEmployeeService>(host.Services);
            service.Run(args);
        }
    }
}
