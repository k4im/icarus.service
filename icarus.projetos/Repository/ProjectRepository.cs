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
        
        public async Task<ProjectResponseDTO> GetProjects(int page = 1)
        {
            var pageResults = 10f;
            var projetos = await _db.Projetos.ToListAsync();
            var pageCount = Math.Ceiling( projetos.Count / pageResults);

            var projects = await _db.Projetos.Skip((page - 1) * (int)pageResults).Take((int)pageResults).ToListAsync();

            var response = new ProjectResponseDTO {
                Projects = projects,
                Pages = (int)pageCount,
                CurrentPage = page,
                PageCount = (int)pageCount
            };

            return response;
        }
        
        public  async Task<ProjectResponseDTO> GetProjectsFilter(string SearchFilter)
        {
            if(!String.IsNullOrEmpty(SearchFilter))
            {
            var projetos = await _db.Projetos.Where( projeto => 
            projeto.Name.ToLower() == SearchFilter.ToLower() || 
            projeto.Status.ToLower() == SearchFilter.ToLower()).ToListAsync();
            // List<ProjectDTO> projects = _mapper.Map<List<ProjectDTO>>(projetos);
            var response = new ProjectResponseDTO {
                Projects = projetos,
                Pages = 0,
                CurrentPage = 0,
                PageCount = 0,
                LastSearch = LastSearchTxt
            };
            return response;
            }
            else {
                var projetos = await _db.Projetos.ToListAsync();
                // List<ProjectDTO> projects = _mapper.Map<List<ProjectDTO>>(projetos);
                var response = new ProjectResponseDTO {
                    Projects = projetos,
                    Pages = 0,
                    CurrentPage = 0,
                    PageCount = 0,
                    LastSearch = LastSearchTxt
                };
                return response;
            }
        }
        private async Task<List<ProjectDTO>> GetProjectsFilterPrivate(string SearchFilter)
        {
            var projetos = await _db.Projetos.ToListAsync();
            List<ProjectDTO> projetosMapper = _mapper.Map<List<ProjectDTO>>(projetos);
            return projetosMapper;
        }
        public async Task<ProjectDTO> CreateProject(Project model)
        {
            if(model != null) 
            {
                try 
                {
                    await _db.Projetos.AddAsync(model);
                    await _db.SaveChangesAsync();
                    var modelDTO = _mapper.Map<ProjectDTO>(model);
                    return modelDTO;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
            } 
            var nullDTO = _mapper.Map<ProjectDTO>(model);
            return nullDTO;
        }

        public async Task DeleteProject(int id)
        {
            try 
            {
                var item = await _db.Projetos.FindAsync(id);
                if(item == null) Results.NotFound();
                _db.Remove(item);
                await _db.SaveChangesAsync();
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
            }
            
        }

        public async Task UpdateProject(Project model, int id)
        {
            if(model != null) 
            {
                try 
                {
                    var item = await _db.Projetos.SingleOrDefaultAsync(x => x.Id == id);
                    if(item == null) Results.NotFound();
                    item.Status = model.Status;
                    _db.Projetos.Update(item);
                    await _db.SaveChangesAsync();
                }
                catch (Exception e){
                    Console.WriteLine(e.Message);
                }
            }
        }
        
        public async Task<ProjectResponseDTO> GetProjectsFilterPagination(string SearchFilter, int page = 1)
        {
            var pageResults = 10f;
            var projetos = await GetProjectsFilterPrivate(SearchFilter);
            var pageCount = Math.Ceiling( projetos.Count / pageResults);

            var projects = await _db.Projetos.Where( projeto => 
            projeto.Name.Contains(SearchFilter.ToLower()) || 
            projeto.Status.Contains(SearchFilter.ToLower())).Skip((page - 1) * (int)pageResults).Take((int)pageResults).ToListAsync();
            var response = new ProjectResponseDTO {
                Projects = projects,
                Pages = (int)pageCount,
                CurrentPage = page,
                PageCount = (int)pageCount,
                LastSearch = LastSearchTxt
            };
            return response;


        }
    }
}