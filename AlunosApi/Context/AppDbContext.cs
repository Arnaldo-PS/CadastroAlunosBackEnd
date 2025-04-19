using AlunosApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlunosApi.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Model.Aluno> Alunos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => e.UserId);
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<Aluno>(entity =>
            {
                entity.ToTable(name: "Alunos");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Idade)
                    .IsRequired();
            });

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
