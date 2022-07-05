using HumanResources.Core.Entities;
using HumanResources.DAL.Context;
using HumanResources.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.DAL.Repositories.Concrete
{
    public class ExpenseRepository : GenericRepository<Expense>, IExpenseDal
    {
        private readonly ApplicationDbContext db;
        public ExpenseRepository(ApplicationDbContext db) : base(db)
        {
            this.db = db;
        }

        public bool ApproveExpense(Expense expense)
        {
            expense.Status = Core.Enums.PermissionStatus.Onaylandi;
            expense.ResponseDate = DateTime.Now.Date;
            db.Expenses.Update(expense);
            return db.SaveChanges() > 0;
        }

        public IEnumerable<Expense> GetAllExpensesById(int id)
        {
            return db.Expenses.Include(e => e.Employee).Where(p => p.EmployeeId == id).ToList();
        }

        public IEnumerable<Expense> GetAllWaitingExpensesWithEmployees(int id)
        {
            return db.Expenses.Include(e => e.Employee).Where(e => e.Status == Core.Enums.PermissionStatus.Bekliyor && e.Employee.CompanyId==id).ToList();
        }

        public IEnumerable<Expense> GetAllWithEmployee()
        {
            return db.Expenses.Include(e => e.Employee).ToList();
        }

        public Expense GetByIdWithEmployee(int id)
        {
            return db.Expenses.Include(e => e.Employee).Where(p => p.Id == id).FirstOrDefault();
        }

        public bool RejectExpense(Expense expense)
        {
            expense.Status = Core.Enums.PermissionStatus.Reddedildi;
            expense.ResponseDate = DateTime.Now.Date;
            db.Expenses.Update(expense);
            return db.SaveChanges() > 0;
        }
    }
}
