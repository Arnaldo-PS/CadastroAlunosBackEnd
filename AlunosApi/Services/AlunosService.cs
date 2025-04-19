using AlunosApi.Context;
using AlunosApi.Model;
using Microsoft.EntityFrameworkCore;

namespace AlunosApi.Services
{
    public class AlunosService : IAlunoService
    {
        private readonly AppDbContext _context;
        public AlunosService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Aluno>> GetAllAlunosAsync()
        {
            try
            {
                return await _context.Alunos.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Aluno> GetAlunoByIdAsync(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            return aluno;
        }

        public async Task<IEnumerable<Aluno>> GetAlunosByNomeAsync(string nome)
        {
            IEnumerable<Aluno> alunos;

            if (!string.IsNullOrEmpty(nome))
            {
                alunos = await _context.Alunos.Where(n => n.Nome.Contains(nome)).ToListAsync();
            }
            else
            {
                alunos = await GetAllAlunosAsync();
            } 
            return alunos;
        }

        public async Task CreateAlunoAsync(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAlunoAsync(Aluno aluno)
        {
            _context.Entry(aluno).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAlunoAsync(Aluno aluno)
        {
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
        }
    }
}
