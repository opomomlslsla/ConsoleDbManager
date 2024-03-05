using PTMK_TestTask.Domain.Entities;
using PTMK_TestTask.Domain.Interfaces;
using System.Diagnostics;

namespace PTMK_TestTask.BuisnessLogic
{
    public class EmployeeService(IEmployeeRepository repository) : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository = repository;

        private string[] maleNames =
            {
            "Oleg", "Vasiliy", "Fedor", "Dmitriy", "Feodosiy", "Kirill", "Nikolay", "Petr", "Igor", "Sergey", "Maxim",
            "Amin", "Daniil", "Artem", "Nikita", "Anatoly", "Andey", "Arseniy", "Pavel", "Mikhail", "Aleksandr", "Alexey", "Roman",
             };

        private string[] maleThirdNames =
        {
            "Olegovich", "Vasilievish", "Fedorpvich", "Dmitrievich", "Feodosievich", "Kirillovich", "Nikolaevich", "Petrovich", "Igorevich", "Sergeevich", "Maximovich",
            "Konstantinovich", "Daniilovich", "Artemovich", "Nikitich", "Anatolievich", "Andeevich", "Arsenievich", "Pavlovich", "Mikhailovich", "Aleksandrovich", "Alexeevich", "Romanovich",
        };

        private string[] maleSecondNames =
        {
            "dubov", "Ivanov", "Makarov", "Mechnikov", "Gorshkov", "Zubarev", "Ulyanov", "Orehov", "Pavlov", "Razumihin", "Karpov", "Kasparov", "Kutuzov", "Molotov", "Alferov",
            "Gagarin", "Turgenev", "Krasnoperov", "Pushkin", "Bebrov", "Smirnov", "Fedorov", "Fayazov", "Fokin", "SheVZov"
        };

        private string[] femaleNames =
        {
            "Galina", "Mariya", "Eva", "Inga", "Ekaterina", "Yana", "Darya", "Ksenia", "Antonina", "Irina", "Polina", "Alexandra", "Yulia", "Svetlana", "Vasilisa", "Nadezhda", "Ulyana",
            "ELizaveta"
        };

        private string[] femaleThirdNames =
        {
            "Olegovna", "Vasilevna", "Fedorovna", "Dmitrievna", "Feodosievna", "Kirillovna", "Nikolaevna", "Petrovna", "Igorevna", "Sergeevna", "Maximovna",
            "Aminovna", "Daniilovna", "Artemovna", "Nikitina", "Anatolievna", "Andeevna", "Arsenievna", "Pavlovna", "Mikhailovna", "Aleksandrovna", "Alexeevna", "Romanovna",

        };

        public async Task Run(string[] args)
        {
            if (args.Length >= 1)
            {
                switch (args[0])
                {
                    case "1":
                        CreateDatabase();
                        Console.WriteLine("Database created!");
                        break;

                    case "2":
                        if (args.Length < 5)
                        {
                            Console.WriteLine("неправильно введены данные");
                            Console.WriteLine("Введите строку в формате: режим(число 1-5) имя фамилия отчество пол дата(формат 24/11/2000)");
                            break;
                        }
                        var strings = args.Skip(1).ToList();
                        await AddEmployee(strings);
                        break;

                    case "3":
                        var employees = GetOrderedEmployees();
                        foreach( var employee in employees)
                        {
                            Console.WriteLine(employee.ToString());
                        }
                        break;
                    case "4":

                        AddRandomEmployees();
                        break;
                    case "5":

                        var stopwatch = new Stopwatch();
                        stopwatch.Start();
                        var employees1 = GetAllEmployeesStartLetterF();
                        stopwatch.Stop();
                        foreach (var employee in employees1)
                        {
                            Console.WriteLine(employee.ToString());
                        }
                        Console.WriteLine("Время выполнения: " + stopwatch.ToString());
                        break;
                }
            }
            else 
            {
                Console.WriteLine("Некорректно введены даные при запуске");
            }
        }

        public async Task AddEmployee(List<string> strings)
        {
            var bdate = DateTime.Parse(strings[4]);
            var employee = CreateEmployee(strings[0], strings[1], strings[2], strings[3], bdate);
            await _employeeRepository.AddEmployee(employee);
        }

        public void AddRandomEmployees()
        {
            var emplist = new List<Employee>();
            var val = Random.Shared.Next(450000, 550000);
            for (int i = 0; i < val; i++)
            {
                var name1 = maleNames[Random.Shared.Next(maleNames.Length - 1)];
                var name2 = maleSecondNames[Random.Shared.Next(maleSecondNames.Length - 1)];
                var name3 = maleThirdNames[Random.Shared.Next(maleThirdNames.Length - 1)];
                var gend = "male";
                var bdate = new DateTime(Random.Shared.Next(1970, 2000), Random.Shared.Next(1, 12), Random.Shared.Next(1, 28));
                emplist.Add(CreateEmployee(name1, name2, name3, gend, bdate));
            }

            for (int i = 0; i < 1000000 - val; i++)
            {
                var name1 = femaleNames[Random.Shared.Next(femaleNames.Length - 1)];
                var name2 = maleSecondNames[Random.Shared.Next(maleSecondNames.Length - 1)] + "a";
                var name3 = femaleThirdNames[Random.Shared.Next(maleThirdNames.Length - 1)];
                var gend = "female";
                var bdate = new DateTime(Random.Shared.Next(1970, 2000), Random.Shared.Next(1, 12), Random.Shared.Next(1, 28));
                emplist.Add(CreateEmployee(name1, name2, name3, gend, bdate));
            }
             _employeeRepository.BulkAddEmployees(emplist);
        }

        public List<Employee> GetAllEmployeesStartLetterF()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var employees = _employeeRepository.GetByExpression(s => s.SecondName.StartsWith("F")).ToList();
            return employees;
        }

        public List<Employee> GetOrderedEmployees()
        {
            var employees = _employeeRepository.GetAllEmployees()
            .GroupBy(e => new { e.FirstName, e.SecondName, e.ThirdName, e.BirthDate })
            .Select(g => g.First())
            .OrderBy(e => e.FirstName)
            .ThenBy(e => e.SecondName)
            .ThenBy(e => e.ThirdName)
            .ToList();
            return employees;
        }

        public void CreateDatabase()
        {
            _employeeRepository.CreateDatabase();
        }

        private Employee CreateEmployee(string name1, string name2, string name3, string gend, DateTime Bdate)
        {
            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = name1,
                SecondName = name2,
                ThirdName = name3,
                Gender = gend,
                BirthDate = Bdate
            };
            return employee;
        }

    }
}
