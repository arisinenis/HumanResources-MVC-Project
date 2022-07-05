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
    public class PackageManager : IPackageService
    {
        private readonly IPackageDal packageRepository;

        public PackageManager(IPackageDal packageRepository)
        {
            this.packageRepository = packageRepository;
        }

        public bool Add(Package entity)
        {
            AddPhoto(entity);
            return packageRepository.Add(entity);
        }

        public bool Delete(Package entity)
        {
            return packageRepository.Delete(entity);
        }

        public bool Exists(int id)
        {
            return packageRepository.Exists(id);
        }

        public IEnumerable<Package> GetAll()
        {
            return packageRepository.GetAll().ToList();
        }

        public Package GetById(int id)
        {
            return packageRepository.GetById(id);
        }

        public bool Update(Package entity)
        {
            AddPhoto(entity);
            return packageRepository.Update(entity);
        }

        // Fotoğraf eklemek için
        private static void AddPhoto(Package entity)
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

        public IEnumerable<Package> GetByUsageAmount(int companyId)
        {
            return packageRepository.GetByUsageAmount(companyId);
        }

        public bool DeleteforPackage(int packageId)
        {
            return packageRepository.DeleteforPackage(packageId);
        }

        public IEnumerable<Package> GetActivePackages()
        {
            return packageRepository.GetActivePackages();
        }
    }
}
