using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UMApi.Models.Menus;

namespace UMApi.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Required]
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Sub> Subs { get; set; }
    }
}
