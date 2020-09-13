using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UMApi.Data;
using UMApi.Dtos;
using UMApi.Models;

namespace UMApi.Services
{
    public interface IRoleService
    {
        void SaveChanges();
        Role Create(Role role);
        Role GetById(int? id);
        IEnumerable<Role> GetAll();        
        void Update(Role user);
        void Delete(int id);
        
    }
    public class RoleService : IRoleService
    {
        private UMContext _dbContext;

        public RoleService(UMContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Role Create(Role role)
        {
            _dbContext.Add(role);
            return role;
        }

        public void Delete(int id)
        {
            Role role = _dbContext.Roles.Where(r => r.Id == id).FirstOrDefault();
            _dbContext.Remove(role);
        }

        public IEnumerable<Role> GetAll()
        {
            return _dbContext.Roles.Include(r => r.Subs).ThenInclude(s => s.MainMenu);
        }

        public Role GetById(int? id)
        {
            Role role = _dbContext.Roles.Include(r => r.Subs).ThenInclude(s => s.MainMenu).Where(r => r.Id == id).FirstOrDefault();
            return role;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Role user)
        {
           //
        }
    }
}
