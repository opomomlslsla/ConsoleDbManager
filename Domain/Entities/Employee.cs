using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMK_TestTask.Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string SecondName { get; set; } = string.Empty;
        public string ThirdName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; } = "male";

        public override string ToString()
        {
            var s = (int)(DateTime.Now - BirthDate).TotalDays / 365;

            return $"{FirstName} {SecondName} {ThirdName} {Gender} возраст: {s} дата рождения: {BirthDate} ";
        }
    }
}
