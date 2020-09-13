using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UMApi.Models.Menus
{
    public class Main
    {
        public int Id { get; set; }
        [Required]
        public string MenuName { get; set; }

        public virtual ICollection <Sub> Subs { get; set; }
    }
}
