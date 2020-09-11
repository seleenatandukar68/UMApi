using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace UMApi.Dtos
{
    public class MainDto
    {
        public int Id { get; set; }
       
        public string MenuName { get; set; }

        [DataMember]
        public ICollection<SubDto> Subs { get; set; }
    }
}
