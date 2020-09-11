using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UMApi.Dtos
{
    public class CreateRoleDto
    {
        public int Id { get; set; }      
        public string RoleName { get; set; }
    }
}
