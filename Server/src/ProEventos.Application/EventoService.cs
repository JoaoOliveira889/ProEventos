﻿using ProEventos.Application.Interfaces;
using ProEventos.Domain;
using ProEventos.Persistence.Interfaces;

namespace ProEventos.Application;
public class EventoService : IEventoService
{
    private readonly IGeralPersist _geralPersist;
    private readonly IEventoPersist _eventoPersist;
    public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
    {
        _geralPersist = geralPersist;
        _eventoPersist = eventoPersist;

    }
    public async Task<Evento> AddEventos(Evento model)
    {
        try
        {
            _geralPersist.Add<Evento>(model);
            if (await _geralPersist.SaveChangesAsync())
            {
                return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
            }
            return null;
        }
        catch (Exception ex)
        {

            throw new ArgumentException(ex.Message);
        }
    }

    public async Task<Evento> UpdateEvento(int id, Evento model)
    {
        try
        {
            var evento = await _eventoPersist.GetEventoByIdAsync(id, false);
            if (evento == null) return null;

            model.Id = evento.Id;

            _geralPersist.Update(model);

            if (await _geralPersist.SaveChangesAsync())
            {
                return await _eventoPersist.GetEventoByIdAsync(model.Id, false);
            }
            return null;
        }
        catch (Exception ex)
        {

            throw new ArgumentException(ex.Message);
        }
    }

    public async Task<bool> DeleteEvento(int id)
    {
        try
        {
            var evento = await _eventoPersist.GetEventoByIdAsync(id, false);
            if (evento == null) throw new Exception("Evento para delete nao econtrado");

            _geralPersist.Delete(evento);

            return await _geralPersist.SaveChangesAsync();
        }
        catch (Exception ex)
        {

            throw new ArgumentException(ex.Message);
        }
    }

    public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
    {
        try
        {
            var eventos = await _eventoPersist.GetAllEventosAsync(includePalestrantes);
            if (eventos == null) return null;

            return eventos;
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException(ex.Message);
        }
    }

    public async Task<Evento> GetAllEventoByIdAsync(int id, bool includePalestrantes = false)
    {
        try
        {
            var eventos = await _eventoPersist.GetEventoByIdAsync(id, includePalestrantes);
            if (eventos == null) return null;

            return eventos;
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException(ex.Message);
        }
    }

    public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
    {
        try
        {
            var eventos = await _eventoPersist.GetAllEventosByTemaAsync(tema, includePalestrantes);
            if (eventos == null) return null;

            return eventos;
        }
        catch (Exception ex)
        {
            throw new ArgumentNullException(ex.Message);
        }
    }


}
