using icarus.estoque.AsyncComm;
using icarus.estoque.Models;
using icarus.estoque.Models.ValueObjects;
using icarus.estoque.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace icarus.estoque.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EstoqueController : ControllerBase
    {
        private readonly IRepoEstoque _repo;
        private readonly IMessageConsumer _msgCosumer;

        public EstoqueController(IRepoEstoque repo, IMessageConsumer msgCosumer)
        {
            _repo = repo;
            _msgCosumer = msgCosumer;
        }

        [HttpGet("produtos/{pagina?}/{resultadoPorPagina?}"), ValidateAntiForgeryToken, Authorize(Roles ="ADMIN, ATENDENTE")]
        public async Task<IActionResult> Produtos(int pagina = 1, float resultadoPorPagina = 5) {
            try
            {
                var consumer = _msgCosumer.consumeMessage();
                if(consumer != null) await _repo.TratarMessage(consumer);   
             
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Não foi possivel consumir: {e.Message}");
            }
            
            await _repo.ValidarProdutos();
            var produtos = await _repo.BuscarProdutos(pagina, resultadoPorPagina);
            if(produtos == null) return NotFound();
            
            return Ok(produtos);
        }

            
        [HttpGet("{id?}"), ValidateAntiForgeryToken, Authorize(Roles ="ADMIN, ATENDENTE")]
        public async Task<IActionResult> Produto(int? id) {
            if(id == null) return BadRequest();
            var produtos = await _repo.BuscarProdutoId(id);
            if(produtos == null) return NotFound();
            return Ok(produtos);
        }


        [HttpPost("produto/novo"), ValidateAntiForgeryToken, Authorize(Roles ="ADMIN, ATENDENTE")]
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


        [HttpDelete("produto/delete/{id?}"), ValidateAntiForgeryToken, Authorize(Roles ="ADMIN, ATENDENTE")]
        public async Task<IActionResult> DeletarProduto(int? id) {
            if(id == null) return BadRequest();
            try
            {
                await _repo.DeletarProduto(id);
                return Ok("Produto deletado com sucesso");
            
            }
            catch(Exception) 
            {
                return StatusCode(409, 
                "Não foi possivel deletar este dado, pois o mesmo foi deletado por um outro usuario!");
            }
        }


        [HttpPut("produto/entrada_produto/{id?}"), ValidateAntiForgeryToken, Authorize(Roles ="ADMIN, ATENDENTE")]
        public async Task<IActionResult> EntradaDeProduto(int? id, Quantidade model) {
            if(id == null) return BadRequest();
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _repo.EntradaDeProdutos(id, model);
                return Ok("Produto atualizado com sucesso");
            
            }
            catch(Exception) 
            {
                return StatusCode(409, 
                "Não foi possivel atualizar este dado, pois o mesmo foi atualizado por um outro usuario!");
            }
            
        }

    }
}