using IdentityAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Infrastructure
{
    public class IdentityDbContext:DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<UserInfo> Users { get; set; }

        public DbSet<Address> Addresses { get; set; }
    }
}
