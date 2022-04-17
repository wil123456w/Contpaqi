using System;

namespace Contpaqi.Models.Empleado
{
    public class CatalogoObj
    {
        public int id { get; set; }
        public string descripcion { get; set; }
        public bool activo { get; set; }
        public DateTime fechaCreacion { get; set; }
        public DateTime fechaModificacion { get; set; }
    }
}
