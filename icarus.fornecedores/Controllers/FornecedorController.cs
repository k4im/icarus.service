using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.fornecedores.Models;
using icarus.fornecedores.Repository;
using Microsoft.AspNetCore.Mvc;

namespace icarus.fornecedores.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedorController : ControllerBase
    {
        private readonly IGenericRepository<Suplly> _repo;
    

        [HttpGet("fornecedores")]
        public IActionResult GetAll(){
            var itens = _repo.GetAll();
            return Ok(itens);
        }

        [HttpGet("fornecedor/{id?}")]
        public async Task<IActionResult> GetById(int? id ){
            if(id == null) return BadRequest();
            var item = await _repo.GetById(id);
            if(item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost("novo-fornecedor")]
        public IActionResult Create(Suplly model) {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try{
                var fornecedor = new Suplly {
                    Name = model.Name,
                    City = model.City,
                    Cep = model.Cep,
                    Cnpj = model.Cnpj,
                    Neighborhood = model.Neighborhood,
                    Phone = model.Phone,
                    HouseNumber = model.HouseNumber,
                    Street = model.Street
                };
            
                _repo.Create(fornecedor);
                return StatusCode(201);
            }
            catch(Exception e){
                Console.WriteLine(e);
            }
            return StatusCode(500);
        }

        [HttpPut("fornecedor/update/{id?}")]
        public IActionResult Update(Suplly model, int? id ){
            if(!ModelState.IsValid) return BadRequest(ModelState);
            if(id == null) return BadRequest();
            try{
                var updtModel = new Suplly {
                    Name = model.Name,
                    City = model.City,
                    Cep = model.Cep,
                    Cnpj = model.Cnpj,
                    Neighborhood = model.Neighborhood,
                    Phone = model.Phone,
                    HouseNumber = model.HouseNumber,
                    Street = model.Street
                };
                _repo.Update(updtModel, id);
                return Ok();
            }
            catch(Exception e){
                Console.WriteLine(e);
            }
            return StatusCode(500);
        }

        [HttpDelete("fornecedor/delete/{id?}")]
        public  IActionResult Delete(int? id){
            if(id == null) return BadRequest();
            try
            {
                _repo.Delete(id);
                return Ok();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return StatusCode(500);
        }
    
    
    }
}