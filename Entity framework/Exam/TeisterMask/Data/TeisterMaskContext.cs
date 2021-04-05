namespace TeisterMask.Data
{
    using Microsoft.EntityFrameworkCore;
    using TeisterMask.Data.Models;

    public class TeisterMaskContext : DbContext
    {
        public TeisterMaskContext() { }

        public TeisterMaskContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeTask> EmployeesTasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeTask>(entity =>
            {

                entity.HasKey(e => new { e.EmployeeId, e.TaskId });

                entity
                .HasOne(x => x.Employee)
                .WithMany(e => e.EmployeesTasks)
                .HasForeignKey(x => x.EmployeeId);

                entity
                .HasOne(x => x.Task)
                .WithMany(t => t.EmployeesTasks)
                .HasForeignKey(x => x.TaskId);
               




            });
        }
    }
}