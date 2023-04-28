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


        public async Task<ProjectResponseDTO> BuscarProdutos(int pagina, float resultadoPorPagina) 
        {
            var ResultadoPorPagina = resultadoPorPagina;
            var projetos = await _db.Projetos.ToListAsync();
            var TotalDePaginas = Math.Ceiling(projetos.Count() / ResultadoPorPagina);
            var projetosPaginados = projetos.Skip((pagina - 1) * (int)ResultadoPorPagina).Take((int)ResultadoPorPagina).ToList();
            
            var response = new ProjectResponseDTO {
                Projects = projetosPaginados,
                Paginas = (int)TotalDePaginas,
                PaginaAtual = pagina,
                TotalDePaginas = (int)TotalDePaginas 
            };
            return response;
        }

        public async Task<Project> BuscarPorId(int? id) 
        {
            var item = await _db.Projetos.FirstOrDefaultAsync(x => x.Id == id);
            if(item == null) return null;
            var response = new Project {
                Id = item.Id,
                Name = item.Name,
                Status = item.Status,
                DataInicio = item.DataInicio,
                DataEntrega = item.DataEntrega,
                Descricao = item.Descricao,
                Valor = item.Valor
            };
            return response;
        }
        
        public async Task CriarProjeto(Project model)
        {
            if (model != null) 
            {
                var project = new Project 
                {
                    Id = model.Id,
                    Name = model.Name,
                    Status = model.Status,
                    Descricao = model.Descricao,
                    DataInicio = model.DataInicio,
                    DataEntrega = model.DataEntrega,
                    QuantidadeDeChapa = model.QuantidadeDeChapa,
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

        public async Task DeletarProjeto(int? id)
        {
            try 
            {
                var item = await _db.Projetos.FirstOrDefaultAsync(x => x.Id == id);
                if(item == null) Results.NotFound();
                _db.Projetos.Remove(item);
                await _db.SaveChangesAsync();
            }
            catch (Exception e){
                Console.WriteLine(e.Message);
            }
            
        }

        public async Task AtualizarProjeto(ProjectUpdate model, int? id)
        {
                if (id != null && model != null) 
                {
                    var item = await _db.Projetos.FirstOrDefaultAsync(x => x.Id == id);
                    if(item == null) Results.NotFound();
                    item.Status = model.Status;
                    _db.Projetos.Update(item);
                    await _db.SaveChangesAsync();
                }
        }
        
    }
}