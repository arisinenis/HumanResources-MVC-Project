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
    public class CompanyManager : ICompanyService
    {
        private readonly ICompanyDal companyRepository;

        public CompanyManager(ICompanyDal companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public bool Add(Company entity)
        {
            AddPhoto(entity);
            return companyRepository.Add(entity);
        }

        public bool Delete(Company entity)
        {
            return companyRepository.Delete(entity);
        }

        public bool Exists(int id)
        {
            return companyRepository.Exists(id);
        }

        public IEnumerable<Company> GetAll()
        {
            return companyRepository.GetAll().ToList();
        }

        public Company GetById(int id)
        {
            return companyRepository.GetById(id);
        }

        public bool Update(Company entity)
        {
            AddPhoto(entity);
            return companyRepository.Update(entity);
        }

        // Fotoğraf eklemek için
        private static void AddPhoto(Company entity)
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

        public int GetpackagesByCompanyID(int id)
        {
            return companyRepository.GetpackagesByCompanyID(id);
        }
    }
}
