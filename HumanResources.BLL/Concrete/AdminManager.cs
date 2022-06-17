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
    public class AdminManager : IAdminService
    {
        private readonly IAdminDal adminRepository;

        public AdminManager(IAdminDal adminRepository)
        {
            this.adminRepository = adminRepository;
        }
        public bool Add(Admin entity)
        {
            AddPhoto(entity);
            return adminRepository.Add(entity);
        }

        public bool Delete(Admin entity)
        {
            return adminRepository.Delete(entity);
        }

        public IEnumerable<Admin> GetAll()
        {
            return adminRepository.GetAll().ToList();
        }

        public Admin GetById(int id)
        {
            return adminRepository.GetById(id);
        }

        public bool Update(Admin entity)
        {
            AddPhoto(entity);
            return adminRepository.Update(entity);
        }

        public bool Exists(int id)
        {
            return adminRepository.Exists(id);
        }

        public Admin GetByEmailAndPassword(string email, string password)
        {
            return adminRepository.GetByEmailAndPassword(email, password);
        }

        // Fotoğraf eklemek için
        private static void AddPhoto(Admin entity)
        {
            if (entity.ProfilePictureFile != null)
            {
                string ticks = DateTime.Now.Ticks.ToString();
                var path1 = Directory.GetCurrentDirectory() + @"\wwwroot\images\" + ticks + Path.GetExtension(entity.ProfilePictureFile.FileName);
                using (var stream = new FileStream(path1, FileMode.Create))
                {
                    entity.ProfilePictureFile.CopyTo(stream);
                }
                entity.ProfilePictureName = @"\images\" + ticks + Path.GetExtension(entity.ProfilePictureFile.FileName);
            }
        }
    }
}
