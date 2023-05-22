using AutoMapper;
using icarus.projetos.data;
using icarus.projetos.models;
using icarus.projetos.models.ValueObject;
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
            return item;
        }
        
        public async Task CriarProjeto(Project model)
        {
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    var projeto = new Project(model.Nome, model.Status, model.DataInicio, model.DataEntrega, model.Chapa, 
                    model.Descricao, model.QuantidadeDeChapa, model.Valor);
                    await _db.AddAsync(projeto);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                    
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Não foi possivel realizar a operação: {e.Message}");
                }
            }
        }

        public async Task DeletarProjeto(int? id)
        {
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try 
                {
                    var item = await _db.Projetos.FirstOrDefaultAsync(x => x.Id == id);
                    if(item == null) Results.NotFound();
                    _db.Projetos.Remove(item);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine("Não foi possivel deletar o dado, tente mais tarde!");
                }
                catch(Exception e)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Não foi possivel estar realizar a operação: {e.Message}");
                }
            }
            
            
        }

        public async Task AtualizarStatus(StatusProjeto model, int? id)
        {
            using (var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    var projeto = await _db.Projetos.FirstOrDefaultAsync(x => x.Id == id);
                    projeto.atualizarStatus(model);
                    await _db.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine("Não foi possivel atualizar o dado, tente mais tarde!");
                }
            }
        }
        
    }
}