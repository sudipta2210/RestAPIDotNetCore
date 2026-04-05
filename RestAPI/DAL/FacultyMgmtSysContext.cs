using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RestAPI.DAL;

public partial class FacultyMgmtSysContext : DbContext
{
    public FacultyMgmtSysContext()
    {
    }

    public FacultyMgmtSysContext(DbContextOptions<FacultyMgmtSysContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TDeptMst> TDeptMsts { get; set; }

    public virtual DbSet<TLoginMaster> TLoginMasters { get; set; }

    public virtual DbSet<TPaperMst> TPaperMsts { get; set; }

    public virtual DbSet<TPaperSemMapping> TPaperSemMappings { get; set; }

    public virtual DbSet<TRoleMst> TRoleMsts { get; set; }

    public virtual DbSet<TSemMst> TSemMsts { get; set; }

    public virtual DbSet<TTeacherLeave> TTeacherLeaves { get; set; }

    public virtual DbSet<TTeacherMst> TTeacherMsts { get; set; }

    public virtual DbSet<TTeacherPaperMapping> TTeacherPaperMappings { get; set; }

    public virtual DbSet<TTimetable> TTimetables { get; set; }

    public virtual DbSet<TUserRoleMapping> TUserRoleMappings { get; set; }

    //Change OnConfiguring
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json") // Specify the configuration file to load.
            .Build(); // Build the configuration object, making it ready to retrieve values.
        var configSection = configBuilder.GetSection("ConnectionStrings");
        var connectionString = configSection["DefaultConnection"] ?? null;
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TDeptMst>(entity =>
        {
            entity.HasKey(e => e.DeptId).HasName("PK__T_dept_M__014881AEAFF5DD93");

            entity.ToTable("T_dept_Mst");

            entity.Property(e => e.DeptName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TLoginMaster>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("PK__T_Login___4DDA2818136432F1");

            entity.ToTable("T_Login_Master");

            entity.HasIndex(e => e.Username, "UQ__T_Login___536C85E4B82F873A").IsUnique();

            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())", "DF__T_Login_M__Creat__5FB337D6")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__T_Login_M__Creat__619B8048");

            entity.HasOne(d => d.ModifiedByNavigation).WithMany(p => p.InverseModifiedByNavigation)
                .HasForeignKey(d => d.ModifiedBy)
                .HasConstraintName("FK__T_Login_M__Modif__628FA481");
        });

        modelBuilder.Entity<TPaperMst>(entity =>
        {
            entity.HasKey(e => e.PaperId).HasName("PK__T_Paper___AB86120B660AEAD3");

            entity.ToTable("T_Paper_Mst");

            entity.Property(e => e.PaperName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TPaperSemMapping>(entity =>
        {
            entity.HasKey(e => e.Tpsmid).HasName("PK__T_Paper___04EC49E536D83AAD");

            entity.ToTable("T_Paper_Sem_Mapping");

            entity.Property(e => e.Tpsmid).HasColumnName("TPSMId");

            entity.HasOne(d => d.Dept).WithMany(p => p.TPaperSemMappings)
                .HasForeignKey(d => d.DeptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_Paper_S__DeptI__6383C8BA");

            entity.HasOne(d => d.Paper).WithMany(p => p.TPaperSemMappings)
                .HasForeignKey(d => d.PaperId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_Paper_S__Paper__6477ECF3");

            entity.HasOne(d => d.Sem).WithMany(p => p.TPaperSemMappings)
                .HasForeignKey(d => d.SemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_Paper_S__SemId__656C112C");
        });

        modelBuilder.Entity<TRoleMst>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__T_Role_M__8AFACE1AF49986F6");

            entity.ToTable("T_Role_Mst");

            entity.HasIndex(e => e.RoleName, "UQ__T_Role_M__8A2B61609407DE95").IsUnique();

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TSemMst>(entity =>
        {
            entity.HasKey(e => e.SemesterId).HasName("PK__T_Sem_Ms__043301DD46360DDD");

            entity.ToTable("T_Sem_Mst");
        });

        modelBuilder.Entity<TTeacherLeave>(entity =>
        {
            entity.HasKey(e => e.LeaveId).HasName("PK__T_Teache__796DB95936FBE6D6");

            entity.ToTable("T_Teacher_Leave");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TTeacherLeaves)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_Teacher__Teach__66603565");
        });

        modelBuilder.Entity<TTeacherMst>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("PK__T_Teache__EDF259642FFE9751");

            entity.ToTable("T_Teacher_Mst");

            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.TeacherName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Dept).WithMany(p => p.TTeacherMsts)
                .HasForeignKey(d => d.DeptId)
                .HasConstraintName("FK__T_Teacher__DeptI__6754599E");
        });

        modelBuilder.Entity<TTeacherPaperMapping>(entity =>
        {
            entity.HasKey(e => e.Tpmid).HasName("PK__T_Teache__68CE49B62B6ECBE6");

            entity.ToTable("T_Teacher_Paper_Mapping");

            entity.Property(e => e.Tpmid).HasColumnName("TPMId");
            entity.Property(e => e.Tpsmid).HasColumnName("TPsmid");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TTeacherPaperMappings)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_Teacher__Teach__68487DD7");

            entity.HasOne(d => d.Tpsm).WithMany(p => p.TTeacherPaperMappings)
                .HasForeignKey(d => d.Tpsmid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_Teacher__TPsmi__693CA210");
        });

        modelBuilder.Entity<TTimetable>(entity =>
        {
            entity.HasKey(e => e.TimetableId).HasName("PK__T_Timeta__68413F60F8069788");

            entity.ToTable("T_Timetable");

            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("created_on");
            entity.Property(e => e.DayOfWeek)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedOn)
                .HasColumnType("datetime")
                .HasColumnName("modified_on");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TTimetables)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_Timetab__Teach__6A30C649");

            entity.HasOne(d => d.Tpsm).WithMany(p => p.TTimetables)
                .HasForeignKey(d => d.TpsmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_Timetab__TpsmI__6B24EA82");
        });

        modelBuilder.Entity<TUserRoleMapping>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__T_User_R__3D978A359080F321");

            entity.ToTable("T_User_Role_Mapping");

            entity.Property(e => e.AssignedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Login).WithMany(p => p.TUserRoleMappings)
                .HasForeignKey(d => d.LoginId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_User_Ro__Login__6C190EBB");

            entity.HasOne(d => d.Role).WithMany(p => p.TUserRoleMappings)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__T_User_Ro__RoleI__6D0D32F4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
