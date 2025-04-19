using AlunosApi.Model;
using AlunosApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private IAlunoService _alunoService;
        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAllAlunos()
        {
            try
            {
                var alunos = await _alunoService.GetAllAlunosAsync();
                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter alunos: {ex.Message}");
            }
        }

        [HttpGet("nome")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunosByNome(string nome)
        {
            try
            {
                var alunos = await _alunoService.GetAlunosByNomeAsync(nome);

                if (alunos == null)
                    return NotFound($"Não existem alunos com o nome: {nome}");

                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter alunos: {ex.Message}");
            }
        }

        [HttpGet("{id:int}", Name = "id")]
        public async Task<ActionResult<Aluno>> GetAlunoById(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAlunoByIdAsync(id);

                if (aluno == null)
                    return NotFound($"Não existe aluno com o id: {id}");

                return Ok(aluno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter alunos: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Aluno>> CreateAluno(Aluno aluno)
        {
            try
            {
                await _alunoService.CreateAlunoAsync(aluno);
                return CreatedAtAction(nameof(GetAlunoById), new { id = aluno.Id }, aluno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter alunos: {ex.Message}");
            }
        }

        [HttpPut("{id:int}", Name = "id")]
        public async Task<ActionResult<Aluno>> UpdateAluno(int id, [FromBody] Aluno aluno)
        {
            try
            {
                if(aluno.Id == id)
                {
                    await _alunoService.UpdateAlunoAsync(aluno);
                    return Ok($"Aluno de nome: {aluno.Nome} atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Dados incosistentes");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter alunos: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}", Name = "id")]
        public async Task<ActionResult> DeleteAluno(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAlunoByIdAsync(id);
                if(aluno != null)
                {
                    await _alunoService.DeleteAlunoAsync(aluno);
                    return Ok($"Aluno {aluno.Nome} excluído com sucesso");
                }
                else
                {
                    return NotFound("Aluno não encontrado");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter alunos: {ex.Message}");
            }
        }
    }
}
