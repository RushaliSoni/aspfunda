using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspfunda.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _emoployeelist;
        public EmployeeRepository()
        {
            _emoployeelist = new List<Employee>()
            {
                new Employee(){Id=1 , Name="Rushali" , Email="rushalilangaliya2@gmail.com" , Department=dept.DigitalMarketing},
                new Employee(){Id=2 , Name="Pratham" , Email="pratham@gmail.com" , Department=dept.Sales },
                new Employee(){Id=3 , Name="Pareshbhai" , Email="pareshbhai@gmail.com" , Department=dept.developer},
                new Employee(){Id=4 , Name="Amishaben" , Email="amishaben@gmail.com" , Department=dept.Supporter}

            };


        }

        public Employee Add(Employee employee)
        {
            employee.Id =_emoployeelist.Max(e => e.Id) + 1;
             _emoployeelist.Add(employee);
            return employee;
        }

        public Employee Delete(int id)
        {
            Employee employee = _emoployeelist.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _emoployeelist.Remove(employee);
            }
            return employee;

        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _emoployeelist;
        }

        public Employee GetEmployee(int Id)
        {
            return _emoployeelist.FirstOrDefault(e => e.Id == Id);
        }
        public int Commit()
        {
            return 0;
        }

        public Employee Update(Employee employeechangs)
        {
            Employee employee = _emoployeelist.FirstOrDefault(e => e.Id == employeechangs.Id);
            if (employee != null)
            {
                employee.Name = employeechangs.Name;
                employee.Email = employeechangs.Email;
                employee.Department = employeechangs.Department;
            }
            return employee;


        }
    }

}
