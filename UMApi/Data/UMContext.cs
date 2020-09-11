using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using UMApi.Models;
using System.Data;
using UMApi.Models.Menus;

namespace UMApi.Data
{
    public class UMContext: DbContext
    {
        public UMContext(DbContextOptions<UMContext> opt) : base(opt)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Main> MainMenu { get; set; }
        public DbSet<Sub> SubMenu { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().
                HasOne(u => u.Role).
                WithMany(r => r.Users);
            modelBuilder.Entity<Main>().
                HasMany(m => m.Subs).
                WithOne(s => s.MainMenu);
            modelBuilder.Entity<Sub>().HasOne(s => s.Role);

        }
        }
    }
