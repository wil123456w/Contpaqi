using Contpaqi.Models.Empleado;

namespace Contpaqi.Models
{
    /// <summary>
    /// Clase que obtiene la Data del request
    /// </summary>
    public class Data
    {
        /// <summary>
        /// Total de registros
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Colección de datos
        /// </summary>
        public Info Info { get; set; }
    }
}
