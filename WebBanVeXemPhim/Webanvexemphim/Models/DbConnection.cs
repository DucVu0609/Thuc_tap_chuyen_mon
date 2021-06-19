namespace Webanvexemphim.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DbConnection : DbContext
    {
        public DbConnection()
            : base("name=ConnectString")
        {
        }
        public virtual DbSet<order> Orders { get; set; }
        public virtual DbSet<ordersdetail> Ordersdetails { get; set; }
        public virtual DbSet<role> Roles { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<contact> contacts { get; set; }
        public virtual DbSet<menu> menus { get; set; }
        public virtual DbSet<movies> movies { get; set; }
        public virtual DbSet<post> posts { get; set; }
        public virtual DbSet<topic> topics { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<category>()
                .Property(e => e.slug)
                .IsUnicode(false);

            modelBuilder.Entity<contact>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<contact>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<menu>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<menu>()
                .Property(e => e.link)
                .IsUnicode(false);

            modelBuilder.Entity<menu>()
                .Property(e => e.position)
                .IsUnicode(false);

            modelBuilder.Entity<movies>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<post>()
                .Property(e => e.slug)
                .IsUnicode(false);

            modelBuilder.Entity<post>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<post>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<topic>()
                .Property(e => e.slug)
                .IsUnicode(false);

            modelBuilder.Entity<topic>()
                .Property(e => e.metakey)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.img)
                .IsUnicode(false);
        }
    }
}
