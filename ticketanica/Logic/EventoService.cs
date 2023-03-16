using System.Collections;
using System.Net.Mime;
using System.Security.Authentication;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MimeMapping;
using ticketanica.DataLayer;
using ticketanicav2.Helpers;
using ticketanicav2.Logic.Interfaces;
using ticketanicav2.Models;
using Entrada = ticketanicav2.Models.Entrada;
using Evento = ticketanicav2.Models.Evento;

namespace ticketanicav2.Logic;

public class EventoService : IEventoService
{
    private readonly TicketanicaDbContext _ticketanicaDb;

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly string img_path = "./wwwroot/images/";

    public EventoService(TicketanicaDbContext ticketanicaDb, IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _ticketanicaDb = ticketanicaDb;
    }

    public List<Evento> GetAll2()
    {
        var eventosDb = _ticketanicaDb.Eventos
            .Include(d => d.IdDireccionNavigation)
            .Include(u => u.EmailOrganizadorNavigation);

        var listaEventos = new List<Evento>();

        foreach (var evento in eventosDb)
        {
            var organizadorModel = new Organizador(evento.EmailOrganizadorNavigation.Email);
            var direccionModel = new Direccion(evento.IdDireccionNavigation.CiudadName,
                evento.IdDireccionNavigation.CalleName, Convert.ToInt32(evento.IdDireccionNavigation.CalleNro),
                evento.IdDireccionNavigation.LocalName);
            
            var imageStream = new MemoryStream(File.ReadAllBytes(img_path + evento.EventoImg));
            var imgEvento = new FormFile(imageStream, 0, imageStream.Length, "image",img_path + evento.EventoImg );
            var eventoModel = new Evento(evento.IdEvento, evento.EventoName, evento.ArtistaName, direccionModel,
                Convert.ToInt32(evento.CapacidadMaxima), organizadorModel,imgEvento);

            listaEventos.Add(eventoModel);
        }

        return listaEventos;
    }
    
    public List<Evento> GetAll()
    {
        var eventosDb = _ticketanicaDb.Eventos
            .Include(d => d.IdDireccionNavigation)
            .Include(u => u.EmailOrganizadorNavigation);

        var listaEventos = new List<Evento>();

        foreach (var evento in eventosDb)
        {
            var organizadorModel = new Organizador(evento.EmailOrganizadorNavigation.Email);
            var direccionModel = new Direccion(evento.IdDireccionNavigation.CiudadName,
                evento.IdDireccionNavigation.CalleName, Convert.ToInt32(evento.IdDireccionNavigation.CalleNro),
                evento.IdDireccionNavigation.LocalName);

            if (File.Exists((img_path + evento.EventoImg)))
            {
                var imageStream = new MemoryStream(File.ReadAllBytes(img_path + evento.EventoImg));
                var imgEvento = new FormFile(imageStream, 0, imageStream.Length, "image", evento.EventoImg)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = MimeUtility.GetMimeMapping(evento.EventoImg)
                };
                var eventoModel = new Evento(evento.IdEvento, evento.EventoName, evento.ArtistaName, direccionModel,
                    Convert.ToInt32(evento.CapacidadMaxima), organizadorModel, imgEvento);

                listaEventos.Add(eventoModel);
            }
            else
            {
                var notFoundStream = new MemoryStream(Encoding.UTF8.GetBytes("Not Found"));
                var imgEvento = new FormFile(notFoundStream, 0, notFoundStream.Length, "image", "Not Found")
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = "image/null"
                    };
                var eventoModel = new Evento(evento.IdEvento, evento.EventoName, evento.ArtistaName, direccionModel,
                    Convert.ToInt32(evento.CapacidadMaxima), organizadorModel, imgEvento);

                listaEventos.Add(eventoModel);
            }
        }

        return listaEventos;
    }

    public Evento GetById(int id)
    {
        var eventoDb = _ticketanicaDb.Eventos
            .Include(d => d.IdDireccionNavigation)
            .Include(e => e.Entrada)
            .FirstOrDefault(e => e.IdEvento == id);
        
        if (eventoDb is null)
            throw new ArgumentException("No existe evento con ese Id");
        
        FormFile imgEvento;
        if (File.Exists((img_path + eventoDb.EventoImg)))
        {
            var imageStream = new MemoryStream(File.ReadAllBytes(img_path + eventoDb.EventoImg));
            imgEvento = new FormFile(imageStream, 0, imageStream.Length, "image", eventoDb.EventoImg)
            {
                Headers = new HeaderDictionary(),
                ContentType = MimeUtility.GetMimeMapping(eventoDb.EventoImg)
            };
        }
        else
        {
            var notFoundStream = new MemoryStream(Encoding.UTF8.GetBytes("Not Found"));
            imgEvento = new FormFile(notFoundStream, 0, notFoundStream.Length, "image", "Not Found")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/null"
            };
        }
        
        var entradas = new Dictionary<string, Entrada>();
        
        foreach (var entrada in eventoDb.Entrada)
        {
            var entradaModel = new Entrada()
            {
                IdEntrada = entrada.IdEntrada,
                CodigoQr = entrada.CodigoQr,
                Usada = entrada.Usada,
                FechaCreacion = entrada.CreatedAt
            };
            entradas.Add(entradaModel.CodigoQr,entradaModel);
        }

        var direccionModel = new Direccion()
        {
            NombreCiudad = eventoDb.IdDireccionNavigation.CiudadName,
            NombreCalle = eventoDb.IdDireccionNavigation.CalleName,
            NombreLocal = eventoDb.IdDireccionNavigation.LocalName,
            NumeroCalle = eventoDb.IdDireccionNavigation.CalleNro,
        };


        return new Evento()
        {
            IdEvento = eventoDb.IdEvento,
            Nombre = eventoDb.EventoName,
            Artista = eventoDb.ArtistaName,
            Entradas = entradas,
            Direccion = direccionModel,
            CapacidadMaxima = eventoDb.CapacidadMaxima,
            Organizador = new Organizador()
            {
                Email = eventoDb.EmailOrganizador
            },
            ImgEvento = imgEvento
        };
    }

    public byte[] GetImageById(int id)
    {
        var eventoDb = _ticketanicaDb.Eventos.FirstOrDefault(e => e.IdEvento == id);

        if (eventoDb == null)
            throw new ArgumentException("No existe evento con ese Id");

        if (!File.Exists(img_path + eventoDb.EventoImg))
            throw new ArgumentException("El archivo de imagen del evento no existe.");

        using var imageStream = new FileStream(img_path + eventoDb.EventoImg, FileMode.Open, FileAccess.Read);
        var memoryStream = new MemoryStream();
        imageStream.CopyTo(memoryStream);

        return memoryStream.ToArray(); // Devuelve la imagen en formato byte[]
    }
   
    

    public int AddEvento(Evento evento)
    {
        if (!_httpContextAccessor.HttpContext.Session.TryGetValue("usuario", out var usuarioSession))
            throw new ArgumentException("No existe ninguna sesión Activa");
        if (evento.ImgEvento is { Length: > 0 })
        {
            using var fileStream = new FileStream(img_path + evento.ImgEvento.FileName, FileMode.Create);
            evento.ImgEvento.CopyTo(fileStream);
        }

        var userSession = Encoding.UTF8.GetString(usuarioSession);
        var userDb = _ticketanicaDb.Users.Find(userSession);
        
        var direccionDb = _ticketanicaDb.Direcciones.FirstOrDefault(d => 
                              d.CalleName == evento.Direccion.NombreCalle &&
                              d.CalleNro == evento.Direccion.NumeroCalle &&
                              d.CiudadName == evento.Direccion.NombreCiudad &&
                              d.LocalName == evento.Direccion.NombreLocal) 
                          ?? new Direccione
                          {
                              CalleName = evento.Direccion.NombreCalle,
                              CalleNro = evento.Direccion.NumeroCalle,
                              CiudadName = evento.Direccion.NombreCiudad,
                              LocalName = evento.Direccion.NombreLocal
                          };

        var eventoDbModel = new ticketanica.DataLayer.Evento
        {
            EventoName = evento.Nombre,
            ArtistaName = evento.Artista,
            CapacidadMaxima = evento.CapacidadMaxima,
            IdDireccionNavigation = direccionDb,
            EmailOrganizadorNavigation = userDb,
            EventoImg = evento.ImgEvento.FileName
        };
        
        
        

        _ticketanicaDb.Eventos.Add(eventoDbModel);
        _ticketanicaDb.SaveChanges();
        return eventoDbModel.IdEvento;
    }

    public bool ModifyEvento(int idEvento)
    {
        throw new NotImplementedException();
    }

  
}
