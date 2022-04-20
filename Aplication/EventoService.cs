using System;
using System.Threading.Tasks;
using API.Domain;
using API.Persistence;

namespace API.Aplication
{
    public class EventoService : IEventoService
    {
        private readonly IEventoPersistence _persist;
        public EventoService(IEventoPersistence persist)
        {
            _persist = persist;
        }
        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                _persist.Add<Evento>(model);

                if(await _persist.SaveChangesAsync()){
                    return await _persist.GetEventoByIdAsync(model.Id);
                }

                return null;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteEvento(int id)
        {
            try
            {
                var evento = await _persist.GetEventoByIdAsync(id);
                if(evento  == null)  throw new Exception("Evento não Encontrado");                 

                _persist.Delete(evento);

                return await _persist.SaveChangesAsync();
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }
        public async Task<Evento> UpdateEvento(int id, Evento model)
        {
            try
            {
                var evento = await _persist.GetEventoByIdAsync(id, false);
                if(evento == null) return null;
                
                
                model.Id = evento.Id;

                _persist.Update(model);

                if(await _persist.SaveChangesAsync())
                {
                    return await _persist.GetEventoByIdAsync(model.Id);
                }

                return null;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosAsync(bool palestrante = false)
        {
            try
            {
                var eventos = await _persist.GetAllEventosAsync(palestrante);
                if(eventos == null) return null;

                return eventos;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }
        public async Task<Evento> GetEventoByIdAsync(int id, bool palestrante = false)
        {
            try
            {
                var evento = await _persist.GetEventoByIdAsync(id, palestrante);
                if(evento == null) return null;

                return evento;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        public async Task<Evento[]> GetEventosByTemaAsync(string tema, bool palestrante = false)
        {
            try
            {
                var evento = await _persist.GetEventosByTemaAsync(tema, palestrante);
                if(evento == null) return null;

                return evento;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

    }
}