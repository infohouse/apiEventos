using System.Linq;
using System.Threading.Tasks;
using API.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Persistence
{
    public class EventoPersistence : IEventoPersistence
    {
        private readonly eventoContext _context;

        public EventoPersistence(eventoContext context)
        {
            _context = context;   
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entity) where T : class
        {
            _context.RemoveRange(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Evento[]> GetAllEventosAsync(bool palestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);
            
            if(palestrante)
            {
                query = query.Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }       

            query = query.OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }
        public async Task<Evento> GetEventoByIdAsync(int id, bool palestrante = false)
        {
               IQueryable<Evento> query = _context.Eventos.AsNoTracking()
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);
            
            if(palestrante)
            {
                query = query.Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }       

            query = query.OrderBy(e => e.Id)
                .Where(e => e.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetEventosByTemaAsync(string tema, bool palestrante = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);
            
            if(palestrante)
            {
                query = query.Include(e => e.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }       

            query = query.OrderBy(e => e.Id)
                .Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool evento = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);
            
            if(evento)
            {
                query = query.Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }
        public async Task<Palestrante> GetPalestranteByIdAsync(int id, bool evento = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);
            
            if(evento){
                query = query.Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p => p.Id)
                .Where(p => p.Id == id);

            return await query.FirstOrDefaultAsync();
        }


        public async Task<Palestrante[]> GetPalestrantesByNomeAsync(string nome, bool evento = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(p => p.RedesSociais);
            
            if(evento)
            {
                query = query.Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p => p.Id)
                .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}