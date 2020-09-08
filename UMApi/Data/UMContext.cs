using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UMApi.Models;

namespace UMApi.Data
{
    public class UMContext: DbContext
    {
        public UMContext(DbContextOptions<UMContext> opt) : base(opt)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
