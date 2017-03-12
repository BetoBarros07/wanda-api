namespace WHTW.Wanda.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AppContext : DbContext
    {
        public AppContext()
            : base("name=AppContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Conversation> Conversation { get; set; }
        public virtual DbSet<Especialization> Especialization { get; set; }
        public virtual DbSet<Hospital> Hospital { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conversation>()
                .HasMany(e => e.Message)
                .WithRequired(e => e.Conversation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Especialization>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Especialization>()
                .HasMany(e => e.Hospital)
                .WithMany(e => e.Especialization)
                .Map(m => m.ToTable("HospitalEspecialization").MapLeftKey("IdEspecialization").MapRightKey("IdHospital"));

            modelBuilder.Entity<Hospital>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Hospital>()
                .HasMany(e => e.Schedule)
                .WithRequired(e => e.Hospital)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.Message1)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.Alergies)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.Medicaments)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.SUSCardId)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.RG)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.MothersName)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.Diseases)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.FamilyHistory)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.AttendenceHistory)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.ProcedureHistory)
                .IsUnicode(false);

            modelBuilder.Entity<Profile>()
                .Property(e => e.ExtraInfos)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.RG)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Conversation)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Profile)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Schedule)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
