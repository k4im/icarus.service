using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using icarus.fornecedores.Models;
using icarus.fornecedores.Repository;
using Microsoft.AspNetCore.Mvc;

namespace icarus.fornecedores.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedorController : ControllerBase
    {
        readonly IRepoFornecedor _repo;
        public FornecedorController(IRepoFornecedor repo)
        {
            _repo = repo;
        }

        [HttpGet("fornecedores/{pagina?}/{resultadoPorPagina?}")]
        public async Task<IActionResult> BuscarFornecedores(int pagina = 1, float resultadoPorPagina = 5)
        {
            try
            {
                var fornecedores = await _repo.BuscarFornecedores(pagina, resultadoPorPagina);
                if(fornecedores == null) return NotFound();
                return Ok(fornecedores);
            }
            catch(Exception e)
            {
                Console.WriteLine($"--> Não foi possivel buscar os fornecedores: {e.Message}");
            }
            return StatusCode(500);
        }
    
        [HttpGet("fornecedores/{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            try
            {
                var fornecedor = await _repo.BuscarPorId(id);
                if(fornecedor == null) return NotFound();
                return Ok(fornecedor);
            }
            catch(Exception e)
            {
                Console.WriteLine($"--> Não foi possivel buscar pelo id: {e.Message}");
            }
            return StatusCode(500);
        }
    
        [HttpPost("fornecedores/novo")]
        public async Task<IActionResult> NovoFornecedor(Fornecedor model)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                // var fornecedor = _mapper.Map<FornecedorDTO, Fornecedor>(model);
                await _repo.CriarFornecedor(model);
                return StatusCode(201, "Fornecedor criado com sucesso!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Não foi possivel criar o fornecedor: {e.Message}");
            }
            return StatusCode(500);
        }

        [HttpPut("fornecedores/atualizar/{id}")]
        public async Task<IActionResult> AtualizarFornecedor(int id, Fornecedor model)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                // var fornecedor = _mapper.Map<FornecedorDTO, Fornecedor>(model);
                await _repo.AtualizarFornecedor(id, model);
                return Ok("Produto atualizado com sucesso!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Não foi possivel criar o fornecedor: {e.Message}");
            }
            return StatusCode(500);
        }

        [HttpDelete("fornecedores/delete/{id}")]
        public async Task<IActionResult> DeletarFornecedor(int id)
        {
            try
            {
                await _repo.DeletarFornecedor(id);
                return Ok("Fornecedor deletado com sucesso!");
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Não foi possivel criar o fornecedor: {e.Message}");
            }
            return StatusCode(500);
        }
    }
}