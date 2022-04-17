using System;

namespace Contpaqi.Models.Empleado
{
    public class EmpleadoObj
    {
        public int empleadoId { get; set; }
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public int edad { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public int generoId { get; set; }
        public int estadoCivilId { get; set; }
        public string rfc { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public int puestoId { get; set; }
        public int? estatusId { get; set; }
        public string puesto { get; set; }
        public DateTime fechaAlta { get; set; }
        public DateTime fechaBaja { get; set; }
    }
}
