using System;
using System.Collections.Generic;

namespace HelpDeskMobile.Shared.Models
{
    public class ListarTicketClienteWCFResult
    {
        public string Cliente { get; set; }
        public string Contacto { get; set; }
        public string Estado { get; set; }
        public object Fecha_Limite_Ticket_Recepcion { get; set; }
        public DateTime Fecha_Limite_Ticket_Recepcion_WCF { get; set; }
        public object Fecha_Limite_Ticket_Soluciona { get; set; }
        public DateTime Fecha_Limite_Ticket_Soluciona_WCF { get; set; }
        public object Fecha_Registro_Ticket { get; set; }
        public DateTime Fecha_Registro_Ticket_WCF { get; set; }
        public object Fecha_Soluciona { get; set; }
        public object Fecha_recepcion_Usuario { get; set; }
        public DateTime Fecha_recepcion_Usuario_WCF { get; set; }
        public string Observaciones { get; set; }
        public string TipoEquipo { get; set; }
        public string TipoServicio { get; set; }
        public object Usuario_Cliente { get; set; }
        public object Usuario_Registra { get; set; }
        public object Usuario_Soluciona { get; set; }
        public object Usuario_recepciona { get; set; }
        public string codigo_Ticket { get; set; }
        public int idTipoEquipo { get; set; }
        public int idTipoServicio { get; set; }
        public int id_Cliente { get; set; }
        public int id_Equipo { get; set; }
        public int id_Servicio_General { get; set; }
        public int id_Ticket { get; set; }
        public int id_contacto { get; set; }
        public string nombre_Equipo { get; set; }
    }

    public class ListaTickets
    {
        public List<ListarTicketClienteWCFResult> ListarTicketClienteWCFResult { get; set; }
    }
}