using System.Threading.Tasks;
using API.Domain;

namespace API.Persistence
{
    public interface IEventoPersistence
    {
         //Geral
         void Add<T>(T entity) where T: class;
         void Update<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         void DeleteRange<T>(T[] entity) where T: class;
         Task<bool> SaveChangesAsync();

        //Eventos

        Task<Evento[]> GetAllEventosAsync(bool palestrante = false);
        Task<Evento> GetEventoByIdAsync(int id, bool palestrante = false);        
        Task<Evento[]> GetEventosByTemaAsync(string tema, bool palestrante = false);        

        
        //Palestrantes

        Task<Palestrante[]> GetAllPalestrantesAsync(bool evento);
        Task<Palestrante> GetPalestranteByIdAsync(int id, bool evento = false);        
        Task<Palestrante[]> GetPalestrantesByNomeAsync(string nome, bool evento = false);        

    }
}