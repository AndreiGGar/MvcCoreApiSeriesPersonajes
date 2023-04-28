using MvcCoreApiSeriesPersonajes.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;

namespace MvcCoreApiSeriesPersonajes.Services
{
    public class ServiceApi
    {
        private MediaTypeWithQualityHeaderValue Header;
        private string UrlApi;

        public ServiceApi(IConfiguration configuration)
        {
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiAzure");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        #region Series

        public async Task<List<Serie>> GetSeriesAsync()
        {
            string request = "/api/Series";
            List<Serie> series = await this.CallApiAsync<List<Serie>>(request);
            return series;
        }

        public async Task<Serie> FindSerieAsync(int id)
        {
            string request = "/api/Series/" + id;
            Serie serie = await this.CallApiAsync<Serie>(request);
            return serie;
        }

        public async Task InsertSerieAsync(string nombre, string imagen, double puntuacion, int anyo)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Series";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Serie serie = new Serie();
                serie.Nombre = nombre;
                serie.Imagen = imagen;
                serie.Puntuacion = puntuacion;
                serie.Anyo = anyo;
                string json = JsonConvert.SerializeObject(serie);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task UpdateSerieAsync(int id, string nombre, string imagen, double puntuacion, int anyo)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Series";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Serie serie =
                    new Serie
                    {
                        IdSerie = id,
                        Nombre = nombre,
                        Imagen = imagen,
                        Puntuacion = puntuacion,
                        Anyo = anyo
                    };
                string json = JsonConvert.SerializeObject(serie);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }

        public async Task DeleteSerieAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Series/" + id;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }

        #endregion

        #region Personajes

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "/api/Personajes";
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task<List<Personaje>> GetPersonajesSerieAsync(int serie)
        {
            string request = "/Series/" + serie;
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task<Personaje> FindPersonajeAsync(int id)
        {
            string request = "/api/Personajes/" + id;
            Personaje personaje = await this.CallApiAsync<Personaje>(request);
            return personaje;
        }

        public async Task InsertPersonajeAsync(string nombre, string imagen, int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Personajes";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje personaje = new Personaje();
                personaje.Nombre = nombre;
                personaje.Imagen = imagen;
                personaje.IdSerie = idserie;
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task UpdatePersonajeAsync(int id, string nombre, string imagen, int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Personajes";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje personaje =
                    new Personaje
                    {
                        IdPersonaje = id,
                        Nombre = nombre,
                        Imagen = imagen,
                        IdSerie = idserie
                    };
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }

        public async Task DeletePersonajeAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/Personajes/" + id;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }

        #endregion
    }
}
