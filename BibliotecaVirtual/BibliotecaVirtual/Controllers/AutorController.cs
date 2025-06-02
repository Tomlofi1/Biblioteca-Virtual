using AutoMapper;
using BibliotecaVirtual.DTO;
using BibliotecaVirtual.Interface;
using BibliotecaVirtual.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaVirtual.Controllers
{
    [ApiController]
    [Route("api/autor")]
    public class AutorController : ControllerBase
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IMapper _mapper;

        public AutorController(IAutorRepository autorRepository, IMapper mapper)
        {
            _autorRepository = autorRepository;
            _mapper = mapper;
        }

        [HttpGet("Autores")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AutorDTO>))]
        public IActionResult GetAutores()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var autores = _autorRepository.GetAutors();

            if (autores == null || !autores.Any())
                return NotFound("Autores nao encontrado");

            var autorDtos = _mapper.Map<IEnumerable<AutorDTO>>(autores);
            return Ok(autorDtos);
        }
        [HttpGet("AutoresPorLivro/{livroId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AutorDTO>))]
        public IActionResult GetAutoresPorLivro(int livroId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var livro = _autorRepository.GetAutorsPorLivro(new Livros { AutorId = livroId });

            if (livro == null || !livro.Any())
                return NotFound("Autores nao encontrado para o livro especificado");

            var autorDtos = _mapper.Map<IEnumerable<AutorDTO>>(livro);
            return Ok(autorDtos);
        }
        [HttpGet("AutoresPorNome/{primeiroNome}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AutorDTO>))]
        public IActionResult GetAutoresPorNome(string primeiroNome)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var autores = _autorRepository.GetAutorsPorNome(primeiroNome);

            if (autores == null || !autores.Any())
                return NotFound("Autores nao encontrado com o nome especificado");

            var autorDtos = _mapper.Map<IEnumerable<AutorDTO>>(autores);
            return Ok(autorDtos);
        }
        [HttpGet("AutorId/{id}")]
        [ProducesResponseType(200, Type = typeof(AutorDTO))]
        public IActionResult GetAutorPorId(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_autorRepository.AutorExiste(id))
                return NotFound("Autor nao encontrado");

            var autor = _autorRepository.GetAutors().FirstOrDefault(a => a.Id == id);
            var autorDto = _mapper.Map<AutorDTO>(autor);
            return Ok(autorDto);
        }
        [HttpPost("CriarAutor")]
        [ProducesResponseType(201, Type = typeof(AutorDTO))]
        [ProducesResponseType(400)]
        public IActionResult CreateAutor([FromBody] AutorDTO autorDto)
        {
            if (autorDto == null)
                return BadRequest(ModelState);

            if (_autorRepository.GetAutors().Any(a => a.PrimeiroNome?.Trim().ToUpper() == autorDto.PrimeiroNome.Trim().ToUpper()))
            {
                ModelState.AddModelError("", "Autor ja existe");
                return StatusCode(422, ModelState);
            }

            var autor = _mapper.Map<Autor>(autorDto);

            if (!_autorRepository.CreateAutor(autor))
            {
                ModelState.AddModelError("", "Erro ao salvar o autor");
                return StatusCode(500, ModelState);
            }

            return CreatedAtAction(nameof(GetAutorPorId), new { id = autor.Id }, autorDto);
        }
        [HttpPut("AtualizarAutor/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAutor(int id, [FromBody] AutorDTO autorDto)
        {
            if (autorDto == null || id != autorDto.Id)
                return BadRequest(ModelState);

            if (!_autorRepository.AutorExiste(id))
                return NotFound("Autor nao encontrado");

            var autor = _mapper.Map<Autor>(autorDto);

            if (!_autorRepository.UpdateAutor(id, autor))
            {
                ModelState.AddModelError("", "Erro ao atualizar o autor");
                return StatusCode(500, ModelState);
            }

            return Ok("Autor atualizado com sucesso");
        }
        [HttpDelete("DeletarAutor/{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAutor(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_autorRepository.AutorExiste(id))
                return NotFound("Autor nao encontrado");

            var autor = _autorRepository.GetAutors().FirstOrDefault(a => a.Id == id);
            if (autor == null)
                return NotFound("Autor nao encontrado");

            if (!_autorRepository.DeleteAutor(id, autor))
            {
                ModelState.AddModelError("", "Erro ao deletar o autor");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpGet("AutoresPorGenero/{genero}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AutorDTO>))]
        public IActionResult GetAutoresPorGenero(string genero)
        {
            if (string.IsNullOrWhiteSpace(genero))
                return BadRequest("Genero nao pode ser vazio");

            var autores = _autorRepository.GetAutorsPorGenero(genero);

            if (autores == null || !autores.Any())
                return NotFound("Autores nao encontrado para o genero especificado");

            var autorDtos = _mapper.Map<IEnumerable<AutorDTO>>(autores);
            return Ok(autorDtos);
        }        
    }
}