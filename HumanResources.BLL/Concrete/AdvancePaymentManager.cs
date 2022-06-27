using HumanResources.BLL.Abstract;
using HumanResources.Core.Entities;
using HumanResources.DAL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.BLL.Concrete
{
    public class AdvancePaymentManager : IAdvancePaymentService
    {
        private readonly IAdvancePaymentDal advancePaymentDal;

        public AdvancePaymentManager(IAdvancePaymentDal advancePaymentDal)
        {
            this.advancePaymentDal = advancePaymentDal;
        }
        public bool Add(AdvancePayment entity)
        {

            return advancePaymentDal.Add(entity);
        }

        public bool ApproveAdvancePayment(AdvancePayment advancePayment)
        {
            return advancePaymentDal.ApproveAdvancePayment(advancePayment);
        }

        public bool Delete(AdvancePayment entity)
        {
            return advancePaymentDal.Delete(entity);
        }

        public bool Exists(int id)
        {
            return advancePaymentDal.Exists(id);
        }

        public IEnumerable<AdvancePayment> GetAll()
        {
            return advancePaymentDal.GetAll();
        }

        public List<AdvancePayment> GetAllWaitingAdvancePayments(int id)
        {
             return advancePaymentDal.GetAllWaitingAdvancePayments(id);
        }

        public List<AdvancePayment> GetByEmployeeId(int id)
        {
            return advancePaymentDal.GetByEmployeeId(id);
        }

        public AdvancePayment GetById(int id)
        {
            return advancePaymentDal.GetById(id);
        }

        public bool RejectAdvancePayment(AdvancePayment advancePayment)
        {
            return advancePaymentDal.RejectAdvancePayment(advancePayment);
        }

        public bool Update(AdvancePayment entity)
        {
            return advancePaymentDal.Update(entity);
        }
    }
}
