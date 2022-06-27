using HumanResources.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.DAL.Repositories.Abstract
{
    public interface IAdvancePaymentDal : IRepository<AdvancePayment>
    {
        List<AdvancePayment> GetByEmployeeId(int id);
        List<AdvancePayment> GetAllWaitingAdvancePayments(int id);
        bool ApproveAdvancePayment(AdvancePayment advancePayment);
        bool RejectAdvancePayment(AdvancePayment advancePayment);
    }
}
