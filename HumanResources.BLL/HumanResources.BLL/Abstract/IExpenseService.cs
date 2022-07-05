using HumanResources.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.BLL.Abstract
{
    public interface IExpenseService : IService<Expense>
    {
        IEnumerable<Expense> GetAllExpensesById(int id);
        IEnumerable<Expense> GetAllWithEmployee();
        Expense GetByIdWithEmployee(int id);
        bool ApproveExpense(Expense expense);
        bool RejectExpense(Expense expense);
        IEnumerable<Expense> GetAllWaitingExpensesWithEmployees(int id);

    }
}
