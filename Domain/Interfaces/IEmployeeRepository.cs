using PTMK_TestTask.Domain.Entities;
using System.Linq.Expressions;

namespace PTMK_TestTask.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employee employee);
        void BulkAddEmployees(List<Employee> eployees);
        void CreateDatabase();
        List<Employee> GetAllEmployees();
        List<Employee> GetByExpression(Expression<Func<Employee, bool>> predicate);
    }
}