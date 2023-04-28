using Microsoft.AspNetCore.Mvc;
using MvcCoreApiSeriesPersonajes.Models;
using MvcCoreApiSeriesPersonajes.Services;

namespace MvcCoreApiSeriesPersonajes.Controllers
{
    public class PersonajesController : Controller
    {
        private ServiceApi service;

        public PersonajesController(ServiceApi service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            return View(personajes);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Personaje personaje)
        {
            await this.service.InsertPersonajeAsync(personaje.Nombre, personaje.Imagen, personaje.IdSerie);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            Personaje personaje = await this.service.FindPersonajeAsync(id);
            return View(personaje);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Personaje personaje)
        {
            await this.service.UpdatePersonajeAsync(personaje.IdPersonaje, personaje.Nombre, personaje.Imagen, personaje.IdSerie);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePersonajeAsync(id);
            return RedirectToAction("Index");
        }
    }
}
