using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult BuscarProdutos(){
            try
            {
                var clientes = _repo.BuscarProdutos();
                return Ok(clientes);
            
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return StatusCode(500);
        }



        [HttpGet("{id?}")]
        public async Task<IActionResult> BuscarProdutos(int? id){
            if(id == null) return BadRequest();
            try
            {
                var cliente = await _repo.BuscarPorId(id);
                return Ok(cliente);

            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return StatusCode(500);

        }

        [HttpPost("create")]
        public  IActionResult CriarProduto(Produto model) {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var produto = new Produto
                {
                    Nome = model.Nome,
                    Cor = model.Cor,
                    Altura = model.Altura,
                    Largura = model.Largura,
                    Quantidade = model.Quantidade,
                    ValorUnitario = model.ValorUnitario,
                };
                _repo.Create(produto);
                return StatusCode(201);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return StatusCode(500);
        } 

    
        [HttpDelete("delete/{id?}")]
        public IActionResult DeletarProduto(int? id){
            if(id == null) return BadRequest();
            try
            {
                _repo.Delete(id);
                return Ok("Item deletado");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return StatusCode(500);
        }

        [HttpPut("update")]
         public IActionResult UpdateProduto(Produto model, int? id) {
            if(!ModelState.IsValid) return BadRequest();
            try
            {
                _repo.Update(model, id);
                return Ok("Produto atualizado");
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return StatusCode(500);
        }
    }
}