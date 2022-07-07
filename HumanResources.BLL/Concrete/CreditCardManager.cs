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
    public class CreditCardManager : ICreditCardService
    {
        private readonly ICreditCardDal creditCardRepository;
        public CreditCardManager(ICreditCardDal creditCardRepository)
        {
            this.creditCardRepository = creditCardRepository;
        }
        public bool Add(CreditCard entity)
        {
            if (entity!=null)
                return creditCardRepository.Add(entity);
            return false;
        }

        public bool Delete(CreditCard entity)
        {
            if (entity!=null)
                return creditCardRepository.Delete(entity);
            return false;
        }

        public bool Exists(int id)
        {
            if (id <0 )
                return creditCardRepository.Exists(id);
            return false;
        }

        public IEnumerable<CreditCard> GetAll()
        {
            return creditCardRepository.GetAll();
        }

        public CreditCard GetById(int id)
        {
            return creditCardRepository.GetById(id);
            
        }

        //public CreditCard GetCreditCardWithCompany(int id) => creditCardRepository.GetCreditCardWithCompany(id);
        

        public bool Update(CreditCard entity)
        {
            if (entity!=null)
                return creditCardRepository.Update(entity);
            return false;
        }
        public IEnumerable<CreditCard> GetAllCreditCardById(int companyId) => creditCardRepository.GetAllCreditCardById(companyId);
       

        
    }
}
