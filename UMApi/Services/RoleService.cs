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

        public Role GetById(int? id)
        {
            Role role = _dbContext.Roles.Where(r => r.Id == id).FirstOrDefault();
            return role;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
