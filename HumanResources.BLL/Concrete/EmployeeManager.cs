using HumanResources.BLL.Abstract;
using HumanResources.Core.Entities;
using HumanResources.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.BLL.Concrete
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IEmployeeDal employeeRepository;
        public EmployeeManager(IEmployeeDal employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public bool Add(Employee entity)
        {
            //if (entity.Photo != null)
            //{
            //    string ticks = DateTime.Now.Ticks.ToString();
            //    var path1 = Directory.GetCurrentDirectory() + @"\wwwroot\images\" + ticks + Path.GetExtension(entity.Photo.FileName);
            //    using (var stream = new FileStream(path1, FileMode.Create))
            //    {
            //        entity.Photo.CopyTo(stream);
            //    }
            //    entity.PhotoPath = @"\images\" + ticks + Path.GetExtension(entity.Photo.FileName);
            //}
            if (entity.BirthDate.AddYears(18) < DateTime.Now)
            {
                AddPhoto(entity);
                return employeeRepository.Add(entity);
            }
            else
                return false;

            return employeeRepository.Add(entity);
        }
        public bool CreateCMAreaEmployee(Employee entity)
        {
            if (entity.Photo != null)
            {
                string ticks = DateTime.Now.Ticks.ToString();
                var path1 = Directory.GetCurrentDirectory() + @"\wwwroot\images\" + ticks + Path.GetExtension(entity.Photo.FileName);
                using (var stream = new FileStream(path1, FileMode.Create))
                {
                    entity.Photo.CopyTo(stream);
                }
                entity.PhotoPath = @"\images\" + ticks + Path.GetExtension(entity.Photo.FileName);
            }
            return employeeRepository.CreateCMAreaEmployee(entity);
        }

        public bool Delete(Employee entity)
        {
            return employeeRepository.Delete(entity);
        }

        public bool Exists(int id)
        {
            return employeeRepository.Exists(id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return employeeRepository.GetAll();
        }

        public Employee GetByEmailAndPassword(string email, string password)
        {
            return employeeRepository.GetByEmailAndPassword(email, password);
        }
        // Fotoğraf eklemek için
        private static void AddPhoto(Employee entity)
        {
            if (entity.Photo != null)
            {
                string ticks = DateTime.Now.Ticks.ToString();
                var path1 = Directory.GetCurrentDirectory() + @"\wwwroot\images\" + ticks + Path.GetExtension(entity.Photo.FileName);
                using (var stream = new FileStream(path1, FileMode.Create))
                {
                    entity.Photo.CopyTo(stream);
                }
                entity.PhotoPath = @"\images\" + ticks + Path.GetExtension(entity.Photo.FileName);
            }
        }

        public Employee GetById(int id)
        {
            return employeeRepository.GetById(id);
        }

        public bool Update(Employee entity)
        {
            return employeeRepository.Update(entity);
        }
    }
}
