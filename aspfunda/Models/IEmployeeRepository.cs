using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspfunda.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int Id);
        IEnumerable<Employee> GetAllEmployee();
        Employee Add(Employee employee);
        Employee Update(Employee employeechangs);
        Employee Delete(int id);
        int Commit();
    }
}
