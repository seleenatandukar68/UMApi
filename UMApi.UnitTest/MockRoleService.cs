using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UMApi.Models;
using UMApi.Models.Menus;
using UMApi.Services;

namespace UMApi.UnitTest
{
    public class MockRoleService : IRoleService
    {
        private List<Role> _roleList;

        public MockRoleService()
        {
            _roleList = new List<Role>
            {
                new Role
                {
                    Id = 1,
                    RoleName = "Guest1",
                    Subs = new List<Sub>
                    {
                        new Sub
                        {
                            SubMenuName ="Save",
                            ControllerName = "SubMenu",
                            Action = "Save",
                            MainMenuId = 1,
                            RoleId = 1,
                            MainMenu = new Main
                            {
                                MenuName = "Files",
                                Id = 1
                            }
                        }
                    }
                },
                new Role
                {
                    Id = 2,
                    RoleName = "Guest2"

                },
                new Role
                {
                    Id = 3,
                    RoleName = "Guest3"

                }
            };
        }

        public Role Create(Role role)
        {

            var item = _roleList[_roleList.Count - 1];
            role.Id = item.Id + 1;
            _roleList.Add(role);
            return role;
        }

        public void Delete(int id)
        {
            var role = _roleList.Where(r => r.Id == id).FirstOrDefault();
            _roleList.Remove(role);
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