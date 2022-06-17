﻿using HumanResources.Core.Entities;
using HumanResources.DAL.Context;
using HumanResources.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.DAL.Repositories.Concrete
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeDal
    {
        private readonly ApplicationDbContext db;

        public EmployeeRepository(ApplicationDbContext db):base(db)
        {
            this.db = db;
        }
        public Employee GetByEmailAndPassword(string email, string password)
        {
            return db.Employees.Where(p => p.Email == email && p.Password == password).FirstOrDefault();
        }
        public bool CreateCMAreaEmployee(Employee _employee)
        {
            try
            {
                var newEmployee = new Employee();
                newEmployee.FirstName = _employee.FirstName;
                newEmployee.LastName = _employee.LastName;
                newEmployee.SecondName = _employee.SecondName;
                newEmployee.CitizenNo = _employee.CitizenNo;
                newEmployee.PhoneNumber = _employee.PhoneNumber;
                newEmployee.Company = _employee.Company;
                newEmployee.Email = _employee.FirstName + "." + _employee.LastName + "@" + _employee.Company.Name + "." + "com";
                newEmployee.Password = _employee.Company.Name + "123"; //Default bir şifre
                newEmployee.Address = _employee.Address;
                newEmployee.BirthDate = _employee.BirthDate;
                newEmployee.StartDate = _employee.StartDate;
                newEmployee.EndDate = _employee.EndDate;
                newEmployee.Status = _employee.Status;
                newEmployee.JobTitle = _employee.JobTitle;
                newEmployee.Job = _employee.Job;
                newEmployee.PhotoPath = _employee.PhotoPath;

                db.Set<Employee>().Add(newEmployee);
                return db.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
