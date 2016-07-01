using System;

namespace HelpDeskMobile.Shared.Models
{
    public class VerificarAccesoClienteResult
    {
        public bool Activo { get; set; }
        public object Area { get; set; }
        public bool Bloqueado { get; set; }
        public string Cargo { get; set; }
        public string Contacto { get; set; }
        public string Empresa { get; set; }
        public object Nombre { get; set; }
        public string Nombres_Cliente { get; set; }
        public string Password { get; set; }
        public object Personal { get; set; }
        public string correo { get; set; }
        public DateTime fechaExpiacion { get; set; }
        public string idUsuario { get; set; }
        public int id_Empresa { get; set; }
        public int id_contacto { get; set; }
        public int id_personal { get; set; }
    }

    public class Usuario
    {
        public VerificarAccesoClienteResult VerificarAccesoClienteResult { get; set; }
    }
}