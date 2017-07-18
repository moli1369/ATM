namespace ATM.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MainModel : DbContext
    {
        public MainModel()
            : base("name=MainModel")
        {
        }

        public virtual DbSet<DateDimension> DateDimensions { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Membership> Memberships { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DateDimension>()
                .Property(e => e.DaySuffix)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DateDimension>()
                .Property(e => e.WeekDayName)
                .IsUnicode(false);

            modelBuilder.Entity<DateDimension>()
                .Property(e => e.HolidayText)
                .IsUnicode(false);

            modelBuilder.Entity<DateDimension>()
                .Property(e => e.MonthName)
                .IsUnicode(false);

            modelBuilder.Entity<DateDimension>()
                .Property(e => e.QuarterName)
                .IsUnicode(false);

            modelBuilder.Entity<DateDimension>()
                .Property(e => e.MMYYYY)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DateDimension>()
                .Property(e => e.MonthYear)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DateDimension>()
                .HasMany(e => e.Documents)
                .WithOptional(e => e.DateDimension)
                .HasForeignKey(e => e.Expire);

            modelBuilder.Entity<DateDimension>()
                .HasMany(e => e.Documents1)
                .WithRequired(e => e.DateDimension1)
                .HasForeignKey(e => e.Submit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DateDimension>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.DateDimension)
                .HasForeignKey(e => e.Start)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DateDimension>()
                .HasMany(e => e.Projects1)
                .WithRequired(e => e.DateDimension1)
                .HasForeignKey(e => e.End)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DateDimension>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.DateDimension)
                .HasForeignKey(e => e.Start)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DateDimension>()
                .HasMany(e => e.Tasks1)
                .WithRequired(e => e.DateDimension1)
                .HasForeignKey(e => e.End)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<File>()
                .HasMany(e => e.People)
                .WithOptional(e => e.File)
                .HasForeignKey(e => e.PictureFileId);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Documents)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Memberships)
                .WithRequired(e => e.Person)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.Person)
                .HasForeignKey(e => e.OwnerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Tasks)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Task>()
                .HasMany(e => e.Memberships)
                .WithRequired(e => e.Task)
                .WillCascadeOnDelete(false);
        }
    }
}
