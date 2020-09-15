using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UMApi.Models;
using UMApi.Services;

namespace UMApiTest.MockServices
{
    class MockRoleService : IRoleService
    {
        private List<Role> _roleList;
      
        public MockRoleService()
        {
            _roleList = new List<Role>()
            {
                new Role
                {
                    Id = 1,
                    RoleName = "Guest1"

                },
                new Role
                {
                    Id = 2,
                    RoleName = "Guest3"

                },
                new Role
                {  
                    Id = 3,
                    RoleName = "Guest3"

                },
                new Role
                {  
                    Id = 4,
                    RoleName = "Guest4"

                }
            };
        }
        public Role Create(Role role)
        {
          
            _roleList.Add(role);
            return role;
        }

        public void Delete(int id)
        {
            var existing = _roleList.Where(r => r.Id == id).FirstOrDefault();
            _roleList.Remove(existing);
        }

        public IEnumerable<Role> GetAll()
        {
            return _roleList;
        }

        public Role GetById(int? id)
        {
            return _roleList.Where(r => r.Id == id).FirstOrDefault(); 
        }

        public void SaveChanges()
        {
           //
        }

        public void Update(Role user)
        {
            //
        }
    }
}
