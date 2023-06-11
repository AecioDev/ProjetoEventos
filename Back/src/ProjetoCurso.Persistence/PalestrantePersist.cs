using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoCurso.Domain;
using ProjetoCurso.Persistence.Contextos;
using ProjetoCurso.Persistence.Contrato;

namespace ProjetoCurso.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {
        public ProjetoCursoContext _context { get; }
        public PalestrantePersist(ProjetoCursoContext context)
        {
            this._context = context;
            
        }
       
        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);

                if(includeEventos){                    
                    query = query
                        .Include(p => p.PalestrantesEventos)
                        .ThenInclude(pe => pe.Evento);
                }

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> NomeAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);

                if(includeEventos){                    
                    query = query
                        .Include(p => p.PalestrantesEventos)
                        .ThenInclude(pe => pe.Evento);
                }

            query = query.AsNoTracking().OrderBy(p => p.Id)
                .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);

                if(includeEventos){                    
                    query = query
                        .Include(p => p.PalestrantesEventos)
                        .ThenInclude(pe => pe.Evento);
                }

            query = query.AsNoTracking().OrderBy(p => p.Id)
                .Where(p => p.Id == palestranteId);

            return await query.FirstOrDefaultAsync(); 
        }        
    }
}