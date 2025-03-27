using AutoMapper;
using BibliotecaVirtual.DTO;
using BibliotecaVirtual.Interface;
using BibliotecaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace BibliotecaVirtual.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class LivrosController : Controller
    {
        private readonly ILivrosRepository _livrosRepository;
        private readonly IMapper _mapper;

        public LivrosController(ILivrosRepository livrosRepository, IMapper mapper)
        {
            _livrosRepository = livrosRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LivrosDTO>))]
        public IActionResult GetLivros()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var Livros = _livrosRepository.GetLivros();

            if (Livros == null)
            {
                return NotFound("Livros nao encontrados");
            }
            return Ok(Livros);

        }
        [HttpGet("genero/{Genero}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<LivrosDTO>))]
        [ProducesResponseType(404)]
        public IActionResult GetLivrosPeloGenero(string Genero)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var livroGenero = _livrosRepository.GetLivrosPeloGenero(Genero);
            if (livroGenero == null)
            {
                return NotFound("Livro de acordo com o genero nao existe!");
            }
            if (!livroGenero.Any())
                return NotFound("Livro de acordo com o genero nao encontrado!");
           
            return Ok(livroGenero);

        }
        [HttpGet("nome/{nome}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Livros>))]
        [ProducesResponseType(404)]
        public IActionResult GetLivrosPeloNome(string nome)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var livroNome = _livrosRepository.GetLivrosPeloNome(nome);
            if(livroNome == null)
            {
                return NotFound("Livro de acordo com o nome nao existe!");
            }
            if (!livroNome.Any())
                return NotFound("Nao existe nenhum livro com este nome em nossa biblioteca");

            return Ok(livroNome);
        }

        [HttpGet("LivroLancamento")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Livros>))]
        [ProducesResponseType(404)]
        public IActionResult GetLivrosPeloLancamento(DateTime dataInicial, DateTime dataFinal)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dataInicial = DateTime.SpecifyKind(dataInicial, DateTimeKind.Utc);
            dataFinal = DateTime.SpecifyKind(dataFinal, DateTimeKind.Utc);

            var livroLancamento = _livrosRepository.GetLivrosPeloLancamento(dataInicial, dataFinal);
            if (livroLancamento == null)
                return NotFound("Data de nascimento");

            if (!livroLancamento.Any())
                return NotFound("Nao existe nenhum livro com esta data");
            
            return Ok(livroLancamento);

        }
        [HttpPost("CriarLivro")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult CreateLivro([FromBody] LivrosDTO livrosCriar)
        {
            if (livrosCriar == null)
                return BadRequest(ModelState);

            var livros = _livrosRepository.GetLivros()
                .Where(c => c.Titulo.Trim().ToUpper() == livrosCriar.Titulo.TrimEnd().ToUpper()).FirstOrDefault();

            if (livros != null)
            {
                ModelState.AddModelError("", "Ja existe um livro com esses dados!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var criarLivros = new Livros
            {
                Titulo = livrosCriar.Titulo.Trim(),
                Genero = livrosCriar.Genero.Trim(),
                LancamentoDoLivro = DateTime.SpecifyKind(livrosCriar.LancamentoDoLivro, DateTimeKind.Utc)
            };

            if (!_livrosRepository.CreateLivro(criarLivros))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de salvar");
                return StatusCode(500, ModelState);
            }

            return Ok("Livro criado com sucesso!");

        }
        [HttpPut("AtualizarLivro")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Livros>))]
        [ProducesResponseType(404)]
        public IActionResult UpdateLivro(int id, [FromBody] LivrosDTO livrosAtualizar)
        {
            if (livrosAtualizar == null)
                return BadRequest(ModelState);
            if (id != livrosAtualizar.Id)
                return BadRequest(ModelState);
            if (!_livrosRepository.LivroExiste(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var livrosUpdateMap = _mapper.Map<Livros>(livrosAtualizar);
            if (!_livrosRepository.UpdateLivro(id, livrosUpdateMap))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de atualizar os livros");
                return StatusCode(500, ModelState);
            }

            return Ok(new { Message = "Livro atualizado com sucesso", Livros = livrosAtualizar});

        }
        [HttpDelete("RemoverLivro")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        
        public IActionResult DeleteLivro(int id, [FromBody] LivrosDTO livroDelete)
        {
            if (livroDelete == null)
                return BadRequest(ModelState);
            if (id != livroDelete.Id)
                return BadRequest(ModelState);
            if (!_livrosRepository.LivroExiste(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var livrosRemoverMap = _mapper.Map<Livros>(livroDelete);
            if (!_livrosRepository.DeleteLivro(id, livrosRemoverMap))
            {
                ModelState.AddModelError("", "Algo deu errado tentando deletar os livros");
                return StatusCode(500, ModelState);
            }

            return Ok(new { Message = "Livro apagado com sucesso", Livros = livroDelete});
        }


    }
}
