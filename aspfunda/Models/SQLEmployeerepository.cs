﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspfunda.Models
{
    public class SQLEmployeerepository : IEmployeeRepository
    {
        private readonly AppDbContext context;
        private readonly ILogger<SQLEmployeerepository> logger;

        public SQLEmployeerepository(AppDbContext context , ILogger<SQLEmployeerepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }
        public int Commit()
        {
            return 0;
        }

        public Employee Delete(int id)
        {
            Employee employee = context.Employees.Find(id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
                 
            }
            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return context.Employees;
        }

        public Employee GetEmployee(int Id)
        {
            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");
            return context.Employees.Find(Id);
        }

        public Employee Update(Employee employeechangs)
        {
            var employee = context.Employees.Attach(employeechangs);
            employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return employeechangs;
        }
    }
}
