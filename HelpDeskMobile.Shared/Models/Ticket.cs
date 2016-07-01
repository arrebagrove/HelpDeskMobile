namespace HelpDeskMobile.Shared.Models
{
    public class TicketInfo
    {
        public string Usuario_Cliente { get; set; }
        public string idTipoServicio { get; set; }
        public string idTipoEquipo { get; set; }
        public string id_Equipo { get; set; }
        public string nombre_Equipo { get; set; }
        public string id_Cliente { get; set; }
        public string Fecha_Registro_Ticket { get; set; }
        public string Observaciones { get; set; }
    }

    public class Ticket
    {
        public TicketInfo T { get; set; }
    }
}