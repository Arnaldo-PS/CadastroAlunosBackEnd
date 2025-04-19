using Microsoft.EntityFrameworkCore;

namespace AlunosApi.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Model.Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.Aluno>().ToTable("Alunos");
            modelBuilder.Entity<Model.Aluno>().HasKey(a => a.Id);
            modelBuilder.Entity<Model.Aluno>().Property(a => a.Nome).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Model.Aluno>().Property(a => a.Email).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Model.Aluno>().Property(a => a.Idade).IsRequired();

            modelBuilder.Entity<Model.Aluno>().HasData(
                new Model.Aluno
                {
                    Id = 1,
                    Nome = "João Silva",
                    Email = "joaosilva@email.alunos.com",
                    Idade = 20
                },
                new Model.Aluno
                {
                    Id = 2,
                    Nome = "Maria Oliveira",
                    Email = "mariaoliveira@email.alunos.com",
                    Idade = 21
                }

            );
        }

    }
}
