namespace Authen_test.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class test_db : DbContext
    {
        public test_db()
            : base("name=test_db")
        {
        }

        public virtual DbSet<member> members { get; set; }
        public virtual DbSet<admin> admins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
