using System.Threading.Tasks;
using API.Domain;

namespace API.Aplication
{
    public interface IEventoService
    {
         Task<Evento> AddEvento(Evento model);
         Task<Evento> UpdateEvento(int id, Evento model);
         Task<bool> DeleteEvento(int id);
         Task<Evento[]> GetAllEventosAsync(bool palestrante = false);
        Task<Evento> GetEventoByIdAsync(int id, bool palestrante = false);        
        Task<Evento[]> GetEventosByTemaAsync(string tema, bool palestrante = false);        
    }
}