using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoCurso.Domain;
using ProjetoCurso.Persistence.Contextos;
using ProjetoCurso.Persistence.Contrato;

namespace ProjetoCurso.Persistence
{
    public class EventoPersist : IEventoPersist
    {
        public ProjetoCursoContext _context { get; }
        public EventoPersist(ProjetoCursoContext context)
        {
            this._context = context;
            
        }
        
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

                if(includePalestrantes){                    
                    query = query
                        .Include(e => e.PalestrantesEventos)
                        .ThenInclude(pe => pe.Palestrante);
                }

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

                if(includePalestrantes){                    
                    query = query
                        .Include(e => e.PalestrantesEventos)
                        .ThenInclude(pe => pe.Palestrante);
                }

            query = query.AsNoTracking().OrderBy(e => e.Id)
                .Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
                
        }

        public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

                if(includePalestrantes){                    
                    query = query
                        .Include(e => e.PalestrantesEventos)
                        .ThenInclude(pe => pe.Palestrante);
                }

            query = query.AsNoTracking().OrderBy(e => e.Id)
                .Where(e => e.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        } 
    }
}