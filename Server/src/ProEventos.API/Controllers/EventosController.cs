using System.Collections;
using System;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Models;
using System.Collections.Generic;
using System.Linq;
using ProEventos.API.Data;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly DataContext _context;
        public EventosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _context.Eventos;
        }

        [HttpGet("{id}")]
        public Evento GetById(int id)
        {
            return _context.Eventos.
            FirstOrDefault(e => e.EventoId == id);
        }
    }
}
