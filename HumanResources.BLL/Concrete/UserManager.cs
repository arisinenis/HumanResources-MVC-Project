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
    public class UserManager : IUserService
    {
        private readonly IUserDal userRepository;

        public UserManager(IUserDal userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool Add(User entity)
        {
            if (entity.BirthDate.AddYears(18) < DateTime.Now)
            {
                AddPhoto(entity);
                return userRepository.Add(entity);
            }
            else
                return false;
            
            
        }

        public bool Delete(User entity)
        {
            return userRepository.Delete(entity);
        }

        public bool Exists(int id)
        {
            return userRepository.Exists(id);
        }

        public IEnumerable<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public User GetByEmailAndPassword(string email, string password)
        {
            return userRepository.GetByEmailAndPassword(email, password);
        }

        public User GetById(int id)
        {
            return userRepository.GetById(id);
        }

        public bool Update(User entity)
        {
            AddPhoto(entity);
            return userRepository.Update(entity);
        }

        // Fotoğraf eklemek için
        private static void AddPhoto(User entity)
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
        public List<Permission> GetManagerWaitingPermissions(int CompanyID)
        {
            return userRepository.GetManagerWaitingPermissions(CompanyID);
        }
    }
}
