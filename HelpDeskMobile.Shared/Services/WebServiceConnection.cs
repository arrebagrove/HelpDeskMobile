using HelpDeskMobile.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HelpDeskMobile.Shared.Services
{
    public class WebServiceConnection
    {
        private static HttpClient http = new HttpClient();
        private static Uri baseUrl = new Uri("http://servicehelpdesk.azurewebsites.net/OperacionesCliente.svc/");

        public static async Task<Usuario> WSLogin(string usuario, string password)
        {
            var completeUrl = new Uri(baseUrl + String.Format("LoginCliente/{0}/{1}", usuario, password));
            var result = await SendRequest(completeUrl);
            return JsonConvert.DeserializeObject<Usuario>(result);
        }

        public static async Task<ListaServicios> WSBusquedaTipoDeServicios(string usuario)
        {
            var completeUrl = new Uri(baseUrl + String.Format("ListarServiciosCliente/{0}/{1}", usuario, DateTime.Now.ToString("yyyy-MM-dd")));
            var result = await SendRequest(completeUrl);
            return JsonConvert.DeserializeObject<ListaServicios>(result);
        }

        public static async Task<ListaTipoEquipos> WSBusquedaTipodeEquipos(string tipo)
        {
            var completeUrl = new Uri(baseUrl + String.Format("ListarTipoEquipoServicio/{0}", tipo));
            var result = await SendRequest(completeUrl);
            return JsonConvert.DeserializeObject<ListaTipoEquipos>(result);
        }

        public static async Task<ListaEquipos> WSBusquedaEquipos(string idServicio, string idTipoEquipo)
        {
            var completeUrl = new Uri(baseUrl + String.Format("ListarEquipoServicios/{0}/{1}", idServicio, idTipoEquipo));
            var result = await SendRequest(completeUrl);
            return JsonConvert.DeserializeObject<ListaEquipos>(result);
        }

        public static async Task<string> WSEnviarIncidencia(Ticket ticket)
        {
            var completeUrl = new Uri(baseUrl + String.Format("GuardarTicket_WCF"));
            var body = JsonConvert.SerializeObject(ticket);
            StringContent stringContent = new StringContent(body, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await http.PostAsync(completeUrl, stringContent);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<string>(jsonResponse);
            }
            return "Error al enviar incidencia";
        }

        public static async Task<bool> WSEnviarCorreo(string correo, string ticket, string empresa, string nombreCliente)
        {
            var completeUrl = new Uri(baseUrl + String.Format("EnviarTicketCliente/{0}/{1}/{2}/{3}",
                                                                correo,
                                                                ticket,
                                                                empresa,
                                                                nombreCliente));
            var result = await SendRequest(completeUrl);
            return JsonConvert.DeserializeObject<bool>(result);
        }

        public static async Task<ListaTickets> WSBusquedaTickets(string username)
        {
            var completeUrl = new Uri(baseUrl + String.Format("ListarTicketCliente/{0}", username));
            var result = await SendRequest(completeUrl);
            return JsonConvert.DeserializeObject<ListaTickets>(result);
        }

        protected static async Task<string> SendRequest(Uri completeUrl)
        {
            var response = await http.GetAsync(completeUrl);
            return await response.Content.ReadAsStringAsync();
        }
    }
}