using Microsoft.AspNetCore.Mvc;
using MvcCoreApiSeriesPersonajes.Models;
using MvcCoreApiSeriesPersonajes.Services;
using System.Numerics;

namespace MvcCoreApiSeriesPersonajes.Controllers
{
    public class SeriesController : Controller
    {
        private ServiceApi service;

        public SeriesController(ServiceApi service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Serie> series = await this.service.GetSeriesAsync();
            return View(series);
        }

        /*public async Task<IActionResult> Details(int id)
        {
            Serie serie = await this.service.FindSerieAsync(id);
            return View(serie);
        }*/

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                List<Personaje> personajes = await this.service.GetPersonajesAsync();
                return View(personajes);
            } else
            {
                List<Personaje> personajes = await this.service.GetPersonajesSerieAsync(id);
                return View(personajes);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Serie serie)
        {
            await this.service.InsertSerieAsync(serie.Nombre, serie.Imagen, serie.Puntuacion, serie.Anyo);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            Serie serie = await this.service.FindSerieAsync(id);
            return View(serie);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Serie serie)
        {
            await this.service.UpdateSerieAsync(serie.IdSerie, serie.Nombre, serie.Imagen, serie.Puntuacion, serie.Anyo);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeleteSerieAsync(id);
            return RedirectToAction("Index");
        }
    }
}
