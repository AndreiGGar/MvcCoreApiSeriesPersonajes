using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcCoreApiSeriesPersonajes.Models
{
    public class Serie
    {
        public int IdSerie { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public double Puntuacion { get; set; }
        public int Anyo { get; set; }
    }
}
