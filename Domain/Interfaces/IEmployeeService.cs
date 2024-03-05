using PTMK_TestTask.Domain.Entities;

namespace PTMK_TestTask.Domain.Interfaces
{
    public interface IEmployeeService
    {
        Task AddEmployee(List<string> strings);
        void AddRandomEmployees();
        void CreateDatabase();
        List<Employee> GetAllEmployeesStartLetterF();
        Task Run(string[] args);
    }
}