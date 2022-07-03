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
    public class WalletManager : IWalletService
    {
        private readonly IWalletDal walletRepository;

        public WalletManager(IWalletDal walletRepository)
        {
            this.walletRepository = walletRepository;
        }

        public bool Add(Wallet entity)
        {
            return walletRepository.Add(entity);
        }

        public bool Delete(Wallet entity)
        {
            return walletRepository.Delete(entity);
        }

        public bool Exists(int id)
        {
            return walletRepository.Exists(id);
        }

        public IEnumerable<Wallet> GetAll()
        {
            return walletRepository.GetAll().ToList();
        }

        public Wallet GetById(int id)
        {
            return walletRepository.GetById(id);
        }

        public Wallet GetWalletWithCompany(int id)
        {
            return walletRepository.GetWalletWithCompany(id);
        }

        public bool Update(Wallet entity)
        {
            return walletRepository.Update(entity);
        }
    }
}
