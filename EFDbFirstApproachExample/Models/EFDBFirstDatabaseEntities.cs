using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace EFDbFirstApproachExample.Models
{
    public class EFDBFirstDatabaseEntities : DbContext
    {
        public EFDBFirstDatabaseEntities()
           : base("name=EFDBFirstDatabaseEntities")
        {
        }

        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}