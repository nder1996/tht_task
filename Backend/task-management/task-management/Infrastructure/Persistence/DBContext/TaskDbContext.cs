using Microsoft.EntityFrameworkCore;
using task_management.Domain.Entities;


namespace task_management.Infrastructure.Persistence.DBContext
{

    /// <summary>
    /// Contexto de base de datos para la gestión de tareas
    /// </summary>
    public class TaskDbContext : DbContext
    {

        /// <summary>
        /// Constructor que recibe las opciones de configuración del contexto
        /// </summary>
        /// <param name="options">Opciones de configuración de Entity Framework</param>
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Conjunto de datos para la entidad de tareas
        /// </summary>
        public DbSet<TasksEntity> Tasks { get; set; }


        /// <summary>
        /// Configuración del modelo de datos durante la creación del contexto
        /// </summary>
        /// <param name="modelBuilder">Constructor de modelos de Entity Framework</param>

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de tablas, relaciones, índices, etc.
            modelBuilder.Entity<TasksEntity>(entity =>
            {
                entity.ToTable("tasks", "public");

                entity.HasKey(e => e.id);

                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.state).HasColumnName("state");
                entity.Property(e => e.title).HasColumnName("title").IsRequired();
                entity.Property(e => e.description).HasColumnName("description");
                entity.Property(e => e.due_date).HasColumnName("due_date");
                entity.Property(e => e.status).HasColumnName("status").IsRequired();
                entity.Property(e => e.create_at).HasColumnName("create_at");
                entity.Property(e => e.update_at).HasColumnName("update_at");
                

                // Define otras relaciones si existen
                // entity.HasOne(e => e.User).WithMany().HasForeignKey("user_id");
            });

            // Agrega configuraciones para otras entidades
        }

    }
}