using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using UMApi.Models.Menus;

namespace UMApi.Dtos
{
    public class CreateRoleDto
    {
       
        public string RoleName { get; set; }
        
        [DataMember]
        public  List <SubDto> Subs { get; set; }
    }
}
