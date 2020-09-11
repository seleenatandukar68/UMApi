using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UMApi.Models.Menus
{
    public class Sub
    {
        public int Id { get; set; }
        public string SubMenuName { get; set; }
        public string ControllerName { get; set; }

        public string Action { get; set; }

        public int MenuId { get; set; }

        public Main MainMenu { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
            
    }
}
