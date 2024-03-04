using PTMK_TestTask.Domain.Entities;

namespace PTMK_TestTask.Domain.Interfaces
{
    public interface IEmployeeService
    {
        void AddEmployee(List<string> strings);
        void AddRandomEmployees();
        void CreateDatabase();
        List<Employee> GetAllEmployeesStartLetterF();
        void Run(string[] args);
    }
}