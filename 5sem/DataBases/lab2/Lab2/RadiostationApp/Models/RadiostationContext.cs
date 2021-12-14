using System;
using Microsoft.EntityFrameworkCore;

namespace RadiostationApp.Models
{
    public partial class RadiostationContext : DbContext
    {
        public RadiostationContext()
        {
        }

        public RadiostationContext(DbContextOptions<RadiostationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BroadcastSchedule> BroadcastSchedules { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Performer> Performers { get; set; }
        public virtual DbSet<Record> Records { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=NIKITAPC\\SQLEXPRESS; Initial Catalog=BDLab1;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<BroadcastSchedule>(entity =>
            {
                entity.ToTable("BroadcastSchedule");

                entity.Property(e => e.DateAndTime).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.BroadcastSchedules)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BroadcastSchedule_Employees");

                entity.HasOne(d => d.Record)
                    .WithMany(p => p.BroadcastSchedules)
                    .HasForeignKey(d => d.RecordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BroadcastSchedule_Records");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Middlename)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GenreName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Performer>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Performers)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Performers_Groups");
            });


            modelBuilder.Entity<Record>(entity =>
            {
                entity.Property(e => e.Album)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ComposName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rating).HasColumnType("decimal(2, 1)");

                entity.Property(e => e.RecordDate).HasColumnType("date");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Records_Genres");

                entity.HasOne(d => d.Performer)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.PerformerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Performers_Records");
            });
        }

        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
