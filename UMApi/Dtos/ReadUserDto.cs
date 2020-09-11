using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using UMApi.Models;

namespace UMApi.Dtos
{
    public class ReadUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }        
        public string LastName { get; set; }       
        public string Username { get; set; }      
        public string Password { get; set; }
        public int RoleId { get; set; }
        
        [DataMember]
        public virtual CreateRoleDto Role { get; set; }

    }
}
