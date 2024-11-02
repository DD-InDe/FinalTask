using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

public partial class EmployeeAdaptationSystemDbContext : DbContext
{
    public EmployeeAdaptationSystemDbContext()
    {
    }

    public EmployeeAdaptationSystemDbContext(DbContextOptions<EmployeeAdaptationSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Collaboration> Collaborations { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeQuestionResult> EmployeeQuestionResults { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<ModuleAccess> ModuleAccesses { get; set; }

    public virtual DbSet<ModuleStatus> ModuleStatuses { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<TestQuestion> TestQuestions { get; set; }

    public virtual DbSet<Testing> Testings { get; set; }

    public virtual DbSet<TestingType> TestingTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LEGION\\DEERSERVER;Database=EmployeeAdaptationSystemDb;Trusted_connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Collaboration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Collabor__3214EC079529E7F7");

            entity.ToTable("Collaboration");

            entity.HasOne(d => d.Employee).WithMany(p => p.Collaborations)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Collabora__Emplo__4316F928");

            entity.HasOne(d => d.Module).WithMany(p => p.Collaborations)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK__Collabora__Modul__4222D4EF");

            entity.HasOne(d => d.Role).WithMany(p => p.Collaborations)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Collabora__RoleI__440B1D61");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC077416EA29");

            entity.ToTable("Department");

            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0794B6A343");

            entity.ToTable("Employee");

            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Login).HasMaxLength(100);
            entity.Property(e => e.MiddleName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Employee__Depart__286302EC");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK__Employee__Positi__29572725");
        });

        modelBuilder.Entity<EmployeeQuestionResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC079CFB8EB0");

            entity.ToTable("EmployeeQuestionResult");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeQuestionResults)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__EmployeeQ__Emplo__5165187F");

            entity.HasOne(d => d.Question).WithMany(p => p.EmployeeQuestionResults)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK__EmployeeQ__Quest__52593CB8");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Event__3214EC0707F0369D");

            entity.ToTable("Event");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Module).WithMany(p => p.Events)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK__Event__ModuleId__35BCFE0A");

            entity.HasOne(d => d.Type).WithMany(p => p.Events)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Event__TypeId__34C8D9D1");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EventTyp__3214EC07D023DB1B");

            entity.ToTable("EventType");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ModuleMa__3214EC075592A305");

            entity.ToTable("Material");

            entity.Property(e => e.Name).HasMaxLength(200);

            entity.HasOne(d => d.Module).WithMany(p => p.Materials)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("Material___fk");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Module__3214EC07AB35676A");

            entity.ToTable("Module");

            entity.Property(e => e.CodeName).HasMaxLength(100);
            entity.Property(e => e.DateCreate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Previous).WithMany(p => p.InversePrevious)
                .HasForeignKey(d => d.PreviousId)
                .HasConstraintName("FK__Module__Previous__2F10007B");

            entity.HasOne(d => d.ResponsiblePerson).WithMany(p => p.Modules)
                .HasForeignKey(d => d.ResponsiblePersonId)
                .HasConstraintName("FK__Module__Responsi__300424B4");

            entity.HasOne(d => d.Status).WithMany(p => p.Modules)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Module__StatusId__2E1BDC42");
        });

        modelBuilder.Entity<ModuleAccess>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ModuleAc__3214EC07843CDD46");

            entity.ToTable("ModuleAccess");

            entity.HasOne(d => d.Module).WithMany(p => p.ModuleAccesses)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK__ModuleAcc__Modul__398D8EEE");

            entity.HasOne(d => d.Position).WithMany(p => p.ModuleAccesses)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK__ModuleAcc__Posit__3A81B327");
        });

        modelBuilder.Entity<ModuleStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ModuleSt__3214EC07249DB4FE");

            entity.ToTable("ModuleStatus");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC07CE009ABF");

            entity.ToTable("Notification");

            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.Text).HasMaxLength(200);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Notificat__Emplo__46E78A0C");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Position__3214EC0734C0BF67");

            entity.ToTable("Position");

            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC070F26EB43");

            entity.ToTable("Role");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<TestQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TestQues__3214EC077AAACFD1");

            entity.ToTable("TestQuestion");

            entity.Property(e => e.Answer).HasMaxLength(150);
            entity.Property(e => e.Question).HasMaxLength(300);

            entity.HasOne(d => d.Testing).WithMany(p => p.TestQuestions)
                .HasForeignKey(d => d.TestingId)
                .HasConstraintName("FK__TestQuest__Testi__4E88ABD4");
        });

        modelBuilder.Entity<Testing>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Testing__3214EC07B4EA4209");

            entity.ToTable("Testing");

            entity.HasOne(d => d.Module).WithMany(p => p.Testings)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("Testing___fk");

            entity.HasOne(d => d.Type).WithMany(p => p.Testings)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Testing__TypeId__4BAC3F29");
        });

        modelBuilder.Entity<TestingType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TestingT__3214EC070D25E218");

            entity.ToTable("TestingType");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
