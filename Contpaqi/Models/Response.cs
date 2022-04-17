namespace Contpaqi.Models
{
    public class Response
    {
        /// <summary>
        /// Colección de datos
        /// </summary>
        public Data Data { get; set; }
        /// <summary>
        /// Indica si la respuesta es correcta
        /// </summary>
        public string IsCorrect { get; set; }
        /// <summary>
        /// Indica mensaje de la respuesta
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Total de registros
        /// </summary>
        public int TotalRegistros { get; set; }
    }
}
