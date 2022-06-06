using System.ComponentModel.DataAnnotations;

namespace Aislamientos.Models
{
    public class Categoria
    {
        public int Id_Categoria { get; set; }
        [Required(ErrorMessage ="El nombre de la categoría es obligatorio")]
        public string Nombre { get; set; }
    }
}
