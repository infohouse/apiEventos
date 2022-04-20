using System;
using System.Threading.Tasks;
using API.Aplication;
using API.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Eventos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class eventoController : ControllerBase
    {

        private readonly IEventoService _eventoService;

        public eventoController(IEventoService eventoService)
        {
            _eventoService = eventoService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if(eventos == null) return NotFound("Eventos não encontrado");

                return Ok(eventos);
            }
            catch (Exception e)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                 $"Erro ao tentar exibir Eventos: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id,true);
                if(evento == null) return NotFound("Evento por id não encontrado");

                return Ok(evento);
            }
            catch (Exception e)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                 $"Erro ao tentar exibir Eventos por id: {e.Message}");
            }
        }

        [HttpGet("tema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var eventos = await _eventoService.GetEventosByTemaAsync(tema);
                if(eventos == null) return NotFound("Eventos por tema não encontrado");

                return Ok(eventos);
            }
            catch (Exception e)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                 $"Erro ao tentar exibir Eventos por tema: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var evento = await _eventoService.AddEvento(model);
                if(evento == null) return BadRequest("Erro ao tentar inserir");

                return Ok(evento);
            }
            catch (Exception e)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                 $"Erro ao tentar Inserir: {e.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,Evento model)
        {
            try
            {
                var evento = await _eventoService.UpdateEvento(id, model);
                if(evento == null) return BadRequest("Erro ao tentar Atualizar");

                return Ok(evento);
            }
            catch (Exception e)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                 $"Erro ao tentar Atualizar: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id,Evento model)
        {
            try
            {
                return await _eventoService.DeleteEvento(id) 
                    ? Ok("Removido com suscesso") 
                    : BadRequest("Erro ao remover este evento");               

            }
            catch (Exception e)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                 $"Erro ao tentar Remover: {e.Message}");
            }
        }


    }//classe
}//namespace
