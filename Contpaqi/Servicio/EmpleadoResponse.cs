using Contpaqi.Resource;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Contpaqi.Servicio
{
    public class EmpleadoResponse<TRequest, TResponse> where TRequest : class where TResponse : class
    {
        private static readonly HttpClient Client = new HttpClient();

        public EmpleadoResponse()
        {
            var baseAddress = new Uri("http://localhost:22206/api/");
            if (Client.BaseAddress == null)
            {
                Client.BaseAddress = baseAddress;
            }
        }

        /// <summary>
        /// Realiza una llamada tipo POST (verbo HTTP) asíncrona, enviandole una solicitud en formato Json
        /// </summary>
        /// <param name="methodAddress">Uri del método del servicio que se va a consumir</param>
        /// <param name="requestHeaders">Diccionario con los headers que se van a incluir en la llamada HTTP</param>
        /// <param name="request">Objeto con los datos de la solicitud que se va a enviar al servicio</param>
        /// <returns>Respuesta (objeto Response) que devuelve el servicio</returns>
        public async Task<TResponse> PostAsJsonAsync(string methodAddress, IDictionary<string, string> requestHeaders, TRequest request)
        {
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(HttpClientResource.HeaderValueAppJson));

            var serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var serializedRequest = JsonConvert.SerializeObject(request, serializerSettings);

            var requestContent = new StringContent(serializedRequest, System.Text.Encoding.UTF8, HttpClientResource.HeaderValueAppJson);

            var responseContent = string.Empty;
            TResponse result = null;

            try
            {
                //////var responseMessage = await Client.PostAsync("personadocumento/buscar", requestContent);
                var responseMessage = await Client.PostAsync(methodAddress, requestContent);
                if (!responseMessage.IsSuccessStatusCode)
                {
                    var returnMessage = await responseMessage.Content.ReadAsStringAsync();
                    responseContent = ProcessError(returnMessage);
                    result = JsonConvert.DeserializeObject<TResponse>(responseContent);
                }
                else
                {
                    responseContent = await responseMessage.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TResponse>(responseContent);
                }


            }
            catch (Exception ex)
            {
                responseContent = ProcessError(ex.Message);
            }


            return result;
        }

        public async Task<TResponse> GetAsJson(string methodAddress)
        {
             TResponse result = null;
            var responseContent = string.Empty;

            try
            {
                var responseMessage = await Client.GetAsync(methodAddress);
                if (!responseMessage.IsSuccessStatusCode)
                {
                    var returnMessage = await responseMessage.Content.ReadAsStringAsync();
                    responseContent = ProcessError(returnMessage);
                    result = JsonConvert.DeserializeObject<TResponse>(responseContent);
                }
                else
                {
                    responseContent = await responseMessage.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<TResponse>(responseContent);
                }
            }
            catch (Exception ex)
            {
                responseContent = ProcessError(ex.Message);
            }

            return result;

        }

        private string ProcessError(string errorMessage)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var baseResponse = "";

            return JsonConvert.SerializeObject(baseResponse, serializerSettings);
        }
    }
}
