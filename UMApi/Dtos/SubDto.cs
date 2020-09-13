using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace UMApi.Dtos
{
    public class SubDto
    {
        public int Id { get; set; }
        public string SubMenuName { get; set; }
        public string ControllerName { get; set; }

        public string Action { get; set; }

        public int MainMenuId { get; set; }

        [DataMember]
        public MainDto MainMenu { get; set; }

        public int RoleId { get; set; }
        
    }
}
