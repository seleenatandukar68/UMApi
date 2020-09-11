using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UMApi.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }        
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

    }
}
