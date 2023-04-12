using icarus.estoque.Models;
using icarus.estoque.Repository;
using Microsoft.AspNetCore.Mvc;

namespace icarus.estoque.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstoqueController : ControllerBase
    {
        private readonly IRepoEstoque _repo;

        public EstoqueController(IRepoEstoque repo)
        {
            _repo = repo;
        }
    
    
        [HttpGet]
        public async Task<IActionResult> Produtos() {
            var produtos = await _repo.BuscarProdutos();
            if(produtos == null) return NotFound();
            return Ok(produtos);
        }

            
        [HttpGet("{id?}")]
        public async Task<IActionResult> Produto(int? id) {
            if(id == null) return BadRequest();
            var produtos = await _repo.BuscarProdutoId(id);
            if(produtos == null) return NotFound();
            return Ok(produtos);
        }


        [HttpPost("produto/novo")]
        public async Task<IActionResult> NovoProduto(Produto model) {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _repo.NovoProduto(model);
                return Ok("Produto criado com sucesso");
            
            }
            catch(Exception e) 
            {
                Console.WriteLine(e);
            }
            return StatusCode(500);
        }


        [HttpDelete("produto/delete/{id?}")]
        public async Task<IActionResult> DeletarProduto(int? id) {
            if(id == null) return BadRequest();
            try
            {
                await _repo.DeletarProduto(id);
                return Ok("Produto deletado com sucesso");
            
            }
            catch(Exception e) 
            {
                Console.WriteLine(e);
            }
            return StatusCode(500);
        }


        [HttpPut("produto/atualizar/{id?}")]
        public async Task<IActionResult> AtualizarProduto(int? id, Produto model) {
            if(id == null) return BadRequest();
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _repo.AtualizarProduto(id, model);
                return Ok("Produto atualizado com sucesso");
            
            }
            catch(Exception e) 
            {
                Console.WriteLine(e);
            }
            return StatusCode(500);
        }
    }
}