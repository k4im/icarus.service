using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using icarus.projetos.data;
using icarus.projetos.models;
using Microsoft.EntityFrameworkCore;

namespace icarus.projetos.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public string LastSearchTxt { get; set; }

        public ProjectRepository(DataContext db, IMapper mapper) 
        {
            _db = db;
            _mapper = mapper;
        }


        public async Task<ProjectResponseDTO> GetAllProjects() 
        {
            var projetos = await _db.Projetos.ToListAsync();
            var response = new ProjectResponseDTO {
                Projects = projetos,
                Pages = 0,
                CurrentPage = 0,
                PageCount = 0 
            };
            return response;
        }
        
        public async Task CreateProject(Project model)
        {
            if (model != null) 
            {
                var project = new Project 
                {
                    Id = model.Id,
                    Name = model.Name,
                    Status = model.Status,
                    Descricao = model.Descricao,
                    DataIncio = model.DataIncio,
                    DataEntrega = model.DataEntrega,
                    Valor = model.Valor
                };
                try
                {

                    _db.Projetos.Add(project);
                   await _db.SaveChangesAsync();
                   
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }  
        }

        public async Task DeleteProject(int id)
        {
            try 
            {
                var item = await _db.Projetos.FindAsync(id);
                if(item == null) Results.NotFound();
                _db.Projetos.Remove(item);
                await _db.SaveChangesAsync();
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
            }
            
        }

        public async Task UpdateProject(Project model, int? id)
        {
                if (id != null && model != null) 
                {
                    var projeto = await _db.Projetos.FindAsync(id);
                    if(projeto == null) Results.NotFound();
                    _db.Entry<Project>(projeto).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                }
        }
        
    }
}