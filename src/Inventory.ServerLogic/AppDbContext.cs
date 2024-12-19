using Inventory.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ServerLogic
{

    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
    {

        public DbSet<Location> Locations { get; set; }

        public DbSet<LocationPermission> LocationPermissions { get; set; }

        public DbSet<Item> Items { get; set; }


    }
}
