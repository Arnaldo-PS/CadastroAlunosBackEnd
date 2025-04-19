using AlunosApi.Model;

namespace AlunosApi.Services
{
    public interface IAlunoService
    {
        Task<IEnumerable<Aluno>> GetAllAlunosAsync();
        Task<Aluno> GetAlunoByIdAsync(int id);
        Task<IEnumerable<Aluno>> GetAlunosByNomeAsync(string nome);
        Task CreateAlunoAsync(Aluno aluno);
        Task UpdateAlunoAsync(Aluno aluno);
        Task DeleteAlunoAsync(Aluno aluno);
    }
}
