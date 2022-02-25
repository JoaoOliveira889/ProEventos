using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application;
public class EventoService : IEventoService
{
    private readonly IGeralPersist _geralPersist;
    private readonly IEventoPersist _eventoPersist;
    public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
    {
        _eventoPersist = eventoPersist;
        _geralPersist = geralPersist;
    }
    public async Task<Evento> AddEventos(Evento model)
    {
        try
        {
            _geralPersist.Add<Evento>(model);

            await _geralPersist.SaveChangesAsync();

            return await _eventoPersist.GetEventoByIdAsync(model.Id, false);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Evento> UpdateEvento(int eventoId, Evento model)
    {
        try
        {
            var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);

            model.Id = evento.Id;

            _geralPersist.Update(model);
            await _geralPersist.SaveChangesAsync();
            return await _eventoPersist.GetEventoByIdAsync(model.Id, false);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteEvento(int eventoId)
    {
        try
        {
            var evento = await _eventoPersist.GetEventoByIdAsync(eventoId, false);
            if (evento == null) throw new Exception("Evento para delete não encontrado.");

            _geralPersist.Delete<Evento>(evento);
            return await _geralPersist.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
    {
        try
        {
            var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);

            return eventos;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
    {
        try
        {
            var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);

            return eventos;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<Evento> GetEventoByIdAsync(int eventoId, bool includePalestrantes = false)
    {
        try
        {
            var eventos = await _eventoPersist.GetEventoByIdAsync(eventoId, includePalestrantes);

            return eventos;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}