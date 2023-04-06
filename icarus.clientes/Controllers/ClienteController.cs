using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using icarus.clientes.Models;
using icarus.clientes.Repository;
using Microsoft.AspNetCore.Mvc;

namespace icarus.clientes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IGenericRepo<Cliente> _repo;

        public ClienteController(IGenericRepo<Cliente> repo)
        {
            _repo = repo;
        }
 
        [HttpGet]
        [Route("clientes")]
        public IActionResult GetAll(){
            try {
                var itens = _repo.GetAll();
                return Ok(itens);
            }
            catch(Exception e){
                Console.WriteLine(e);
            }
            return StatusCode(500);
        }

        [HttpGet]
        [Route("cliente/{id?}")]
        public async Task<IActionResult> GetById(int? id){
            if(id == null) return BadRequest();
            try {
                var item = await _repo.GetById(id);
                return Ok(item);

            }
            catch(Exception e){
                Console.WriteLine(e);
            }
            return NotFound();
        }
 
    
        [HttpPost]
        [Route("novo-cliente")]
        public IActionResult Create(Cliente model){
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try {
                var cliente = new Cliente {
                    Name = model.Name,
                    Phone = model.Phone,
                    City = model.City,
                    Neighborhood = model.Neighborhood,
                    Street = model.Street,
                    Cep = model.Cep,
                    HouseNumber = model.HouseNumber,

                };
                _repo.Create(cliente);
                return StatusCode(201, model);
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
            return StatusCode(500);
        }


        [HttpDelete]
        [Route("delete/{id?}")]
        public IActionResult Delete(int? id) {
            if(id == null) return BadRequest();
            try{
                _repo.Delete(id);
                return Ok();
            }
            catch(Exception e) {
                Console.WriteLine(e);
            }
            return StatusCode(500);
        }


        [HttPut]
        [Route("update/{id?}")]
        public IActionResult Update(Cliente model, int? id) {
            if(id == null) return BadRequest();
            if(!ModelState.IsValid) return BadRequest(ModelState);
            try{
                _repo.Update(model, id);
                return  Ok();
            }
            catch(Exception e){
                Console.WriteLine(e);
            }
            return StatusCode(500);
        }
    }
}