using AutoMapper;
using BibliotecaVirtual.DTO;
using BibliotecaVirtual.Interface;
using BibliotecaVirtual.Models;
using BibliotecaVirtual.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaVirtual.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        public ClienteController(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }
        [HttpGet("Clientes")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClienteDTO>))]

        public IActionResult GetClientes()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Clientes = _clienteRepository.GetClientes();

            if (Clientes == null)
                return NotFound("Clientes nao encontrado");

            var clienteDtos = _mapper.Map<IEnumerable<ClienteDTO>>(Clientes);

            return Ok(clienteDtos);
        }
        [HttpGet("ClienteDataNascimento")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClienteDTO>))]
        [ProducesResponseType(404)]

        public IActionResult GetClientesPelaDataDeNascimento(DateTime dataInicial, DateTime dataFinal)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dataInicial = DateTime.SpecifyKind(dataInicial, DateTimeKind.Utc);
            dataFinal = DateTime.SpecifyKind(dataFinal, DateTimeKind.Utc);

            var clienteDatadeNascimento = _clienteRepository.GetClientesPelaDatadeNascimento(dataInicial, dataFinal);
            if (clienteDatadeNascimento == null)
                return NotFound("Data de Nascimento nao encontrada");

            if (!clienteDatadeNascimento.Any())
                return NotFound("Nao tem nenhum cliente com essa data de nascimento");

            var clienteNascimentodto = _mapper.Map<IEnumerable<ClienteDTO>>(clienteDatadeNascimento);
            
            return Ok(clienteNascimentodto);
        }

        [HttpGet("ClienteId")]
        [ProducesResponseType(200, Type = typeof(ClienteDTO))]
        [ProducesResponseType(404)]
        public IActionResult GetClientesPeloId(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clienteId = _clienteRepository.GetClientesPeloId(id);
            if (clienteId == null)
                return NotFound("Nao possui nenhum cliente com esta ID");

            var clienteDtoId = _mapper.Map<ClienteDTO>(clienteId);

            return Ok(clienteDtoId);
        }

        [HttpPost("CriarCliente")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult CreateCliente([FromBody] ClienteDTO clienteCriar)
        {
            if (clienteCriar == null)
                return BadRequest(ModelState);

            var cliente = _clienteRepository.GetClientes().Where(p => p.PrimeiroNome.Trim().ToUpper() == clienteCriar.PrimeiroNome.Trim().ToUpper()).FirstOrDefault();

            if (cliente != null)
            {
                ModelState.AddModelError("", "Ja existe um cliente com essas caracteristicas");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var criarCliente = new Cliente
            {
                PrimeiroNome = clienteCriar.PrimeiroNome,
                SegundoNome = clienteCriar.SegundoNome,
                DataDeNascimento = DateTime.SpecifyKind(clienteCriar.DataDeNascimento, DateTimeKind.Utc)
            };

            if (!_clienteRepository.CreateCliente(criarCliente))
            {
                ModelState.AddModelError("", "Deu errado ao tentar criar o novo cliente");
                return StatusCode(500, ModelState);
            }

            return Ok("Cliente criado com sucesso");
        }

        [HttpPut("AtualizarCliente")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCliente(int id, [FromBody] ClienteDTO clienteUpdate)
        {
            if (clienteUpdate == null)
                return BadRequest(ModelState);
            if (id != clienteUpdate.Id)
                return BadRequest(ModelState);
            if (!_clienteRepository.ClienteExiste(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clienteUpdateMap = _mapper.Map<Cliente>(clienteUpdate);

            if (!_clienteRepository.UpdateCliente(id, clienteUpdateMap))
            {
                ModelState.AddModelError("", "Algo deu errado na hora de atualizar os clientes");
                return StatusCode(500, ModelState);
            }
            return Ok(new { Message = "Cliente atualizado com sucesso", Cliente = clienteUpdate });
        }

        [HttpDelete("RemoverCliente")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult RemoveCliente(int id, [FromBody] ClienteDTO removerCliente)
        {
            if (removerCliente == null)
                return BadRequest(ModelState);
            if (id != removerCliente.Id)
                return BadRequest(ModelState);
            if (!_clienteRepository.ClienteExiste(id))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clienteRemoverMap = _mapper.Map<Cliente>(removerCliente);
            if (!_clienteRepository.DeleteCliente(id, clienteRemoverMap))
            {
                ModelState.AddModelError("", "Algo deu errado ao apagar os clientes");
                return StatusCode(500, ModelState);
            }
            return Ok(new { Message = "Cliente Apagado com sucesso", Cliente = removerCliente });
        }
    }
}
