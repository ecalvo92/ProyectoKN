using KN_ProyectoClase.BaseDatos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace KN_ProyectoClase.Models
{
	public class Utilitarios
	{
        public bool EnviarCorreo(string correo, string mensaje, string titulo)
        {
            string cuenta = ConfigurationManager.AppSettings["CorreoNotificaciones"].ToString();
            string contrasenna = ConfigurationManager.AppSettings["ContrasennaNotificaciones"].ToString();

            MailMessage message = new MailMessage();
            message.From = new MailAddress(cuenta);
            message.To.Add(new MailAddress(correo));
            message.Subject = titulo;
            message.Body = mensaje;
            message.Priority = MailPriority.Normal;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.office365.com", 587);
            client.Credentials = new System.Net.NetworkCredential(cuenta, contrasenna);
            client.EnableSsl = true;
            //client.Send(message);
            return true;
        }
    }
}