using MailKit.Net.Smtp;
using MimeKit;

namespace Bionuclear.Infrastructure.Persistence
{
    public static class Correo
    {
        public static void enviar_correo(String Servidor, int Puerto, String GmailUser, String GmailPass, string Destino, string cuerpo = "Hola <b>Bionuclear<b>")
        {

            MimeMessage mensaje = new();
            mensaje.From.Add(new MailboxAddress("Bionuclear Web", GmailUser));
            mensaje.To.Add(new MailboxAddress("Destino", Destino));
            mensaje.Subject = "Acceso a sus resultados web";

            BodyBuilder CuerpoMensaje = new();
            CuerpoMensaje.HtmlBody = cuerpo;

            mensaje.Body = CuerpoMensaje.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(Servidor, Puerto, false);
                // Note: only needed if the SMTP server requires authentication
                client.Authenticate(GmailUser, GmailPass);
                client.Send(mensaje);
                client.Disconnect(true);
            }


        }
    }
}
