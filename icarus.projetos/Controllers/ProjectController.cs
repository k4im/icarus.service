using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using icarus.projetos.AsyncComm;
using icarus.projetos.models;
using icarus.projetos.models.ModelsShared;
using icarus.projetos.models.ValueObject;
using icarus.projetos.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace icarus.projetos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class ProjectController : ControllerBase
    {
        readonly IProjectRepository _repo;
        readonly IMessageBusService _msgBus;
        readonly IMapper _mapper;
        readonly ILogger<ProjectController> _logger;
        public ProjectController(IProjectRepository repo, 
        IMessageBusService msgBus, IMapper mapper,
        ILogger<ProjectController> logger)
        {
            _repo = repo;
            _msgBus = msgBus;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("projetos/{pagina?}/{resultadoPorPagina?}")]
        public async Task<IActionResult> GetAllProjects(int pagina = 1, float resultadoPorPagina = 5) 
        {
            _logger.LogInformation($"Realizando operação GET: {DateTime.UtcNow}");
            var projetos = await _repo.BuscarProdutos(pagina, resultadoPorPagina);
            return Ok(projetos);
        }
        
        [HttpGet("projeto/{id?}")]
        public async Task<IActionResult> GetById(int? id) 
        {
            var item = await _repo.BuscarPorId(id);
            if(item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateProject(Project model)
        {
            
            if(ModelState.IsValid) 
            {
                await _repo.CriarProjeto(model);
                try 
                {
                    var projeto = _mapper.Map<Project, PublishProject>(model);
                    _msgBus.publishNewProjeto(projeto);
                    Console.WriteLine("--> Mensagem enviado para o bus");
                }
                catch(Exception e)
                {
                    _logger.LogError($"[{DateTime.UtcNow}] Não foi possivel enviar a mensagem para o bus: {e.Message}");
                    Console.WriteLine($"--> Não foi possivel enviar a mensagem para o bus: {e.Message}");
                }
            }
            return StatusCode(201);
        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateProject( StatusProjeto model, int? id)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState); 
            if(id == null) return NotFound();
            try 
            {
                await _repo.AtualizarStatus(model, id);
                return Ok();
            }
            catch(Exception)
            {
                return StatusCode(409, "Não foi possivel atualizar o item, o mesmo foi atualizado por outro usuario!");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteProject(int? id)
        {
            if(id == null) return NotFound();
            try
            {
                await _repo.DeletarProjeto(id);
                return StatusCode(204);
            }
            catch (Exception)
            {
                return StatusCode(409, "Não foi possivel deletar o item, o mesmo foi deletado por outro usuario!");
            }
        }
    

    }
}