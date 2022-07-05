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
    public class ExpenseManager : IExpenseService
    {
        private readonly IExpenseDal expenseRepository;

        public ExpenseManager(IExpenseDal expenseRepository)
        {
            this.expenseRepository = expenseRepository;
        }

        public bool Add(Expense entity)
        {
            AddFile(entity);
            return expenseRepository.Add(entity);   
        }

        public bool Delete(Expense entity)
        {
            return expenseRepository.Delete(entity);
        }

        public bool Exists(int id)
        {
            return expenseRepository.Exists(id);
        }

        public IEnumerable<Expense> GetAll()
        {
            return expenseRepository.GetAll().ToList();
        }

        public Expense GetById(int id)
        {
            return expenseRepository.GetById(id);
        }

        public bool Update(Expense entity)
        {
            AddFile(entity);
            return expenseRepository.Update(entity);
        }

        public IEnumerable<Expense> GetAllExpensesById(int id)
        {
            return expenseRepository.GetAllExpensesById(id);
        }

        // Dosya eklemek için
        private static void AddFile(Expense entity)
        {
            if (entity.File != null)
            {
                string ticks = DateTime.Now.Ticks.ToString();
                var path1 = Directory.GetCurrentDirectory() + @"\wwwroot\files\" + ticks + Path.GetExtension(entity.File.FileName);
                using (var stream = new FileStream(path1, FileMode.Create))
                {
                    entity.File.CopyTo(stream);
                }
                entity.FileName = @"\files\" + ticks + Path.GetExtension(entity.File.FileName);
            }
        }

        public Expense GetByIdWithEmployee(int id)
        {
            return expenseRepository.GetByIdWithEmployee(id);
        }

        public bool ApproveExpense(Expense expense)
        {
            return expenseRepository.ApproveExpense(expense);
        }

        public bool RejectExpense(Expense expense)
        {
            return expenseRepository.RejectExpense(expense);
        }

        public IEnumerable<Expense> GetAllWaitingExpensesWithEmployees(int id)
        {
            return expenseRepository.GetAllWaitingExpensesWithEmployees(id);
        }

        public IEnumerable<Expense> GetAllWithEmployee()
        {
            return expenseRepository.GetAllWithEmployee();
        }
    }
}
