using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using PTMK_TestTask.Domain.Entities;
using PTMK_TestTask.Domain.Interfaces;
using System.Linq;
using System.Linq.Expressions;

namespace PTMK_TestTask.Ifrastructure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly Context _context;
        public EmployeeRepository(Context context)
        {
            _context = context;
        }

        public async Task AddEmployee(Employee employee)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();
        }

        public void CreateDatabase()
        {
            _context.CreateDatabase();
        }

        public void BulkAddEmployees(List<Employee> eployees)
        {
             _context.BulkInsert(eployees);
             _context.BulkSaveChanges();
        }

        public List<Employee> GetByExpression(Expression<Func<Employee, bool>> predicate)
        {
            return _context.Employees.Where(predicate).ToList();
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }

    }
}
