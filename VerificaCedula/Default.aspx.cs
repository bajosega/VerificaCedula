using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Hosting;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tesseract;

namespace VerificaCedula
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [WebMethod]
        public static bool Verificar(string NombreArchivo,  string numero)
        {

            bool resultado = false;
            string ruta = ConfigurationManager.AppSettings["ruta_imagenes"].Trim();
           //NombreArchivo = "10002-D1.jpg";
            Debug.WriteLine("clear");

          
            numero = numero.Replace(".", string.Empty);
            numero = numero.Replace(",", string.Empty);

            try
            {
                // PROCESAR IMAGEN
                string rutaTessdata = Path.Combine(HostingEnvironment.MapPath("/"), "tessdata");
                string text;
                using (var engine = new TesseractEngine(rutaTessdata, "spa", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(ruta + NombreArchivo))
                    {
                        using (var page = engine.Process(img))
                        {
                            text = page.GetText();
                            Debug.WriteLine(text);
                        }
                    }
                }

                text = text.Replace(".", string.Empty);
                text = text.Replace(",", string.Empty);

                if (numero.Trim() != "" && text.Contains(numero))
                {
                    Debug.WriteLine("Numero Encontrado");
                    resultado = true;
                }

                Debug.WriteLine($"Resultado={resultado}");
            }

            catch (Exception e)
            {
                Debug.WriteLine("Unexpected Error: " + e.Message);
                Debug.WriteLine("Details: ");
                Debug.WriteLine(e.ToString());
            }

          
            //Debug.WriteLine("la Cedula parece ser correcta");
            return resultado;

        }

        //private void correo()
        //{

        //    String FROM = ConfigurationManager.AppSettings["from"].Trim();
        //    String FROMNAME = ConfigurationManager.AppSettings["fromName"].Trim();
        //    String TO = ConfigurationManager.AppSettings["to"].Trim();
        //    String SMTP_USERNAME = ConfigurationManager.AppSettings["userName"].Trim();
        //    String SMTP_PASSWORD = ConfigurationManager.AppSettings["password"].Trim();
        //    String CONFIGSET = "ConfigSet";
        //    String HOST = ConfigurationManager.AppSettings["host"].Trim();
        //    int PORT = Convert.ToInt32(ConfigurationManager.AppSettings["port"].Trim());
        //    String SUBJECT = ConfigurationManager.AppSettings["asunto"].Trim();

        //    // The body of the email
        //    String BODY =
        //        $@"<h1>Se cargo una Cedula que no cumple con el % Configurado</h1>
        //        <p> 
        //        <a href=''></a> 
        //        </p>";

        //    // Create and build a new MailMessage object
        //    MailMessage message = new MailMessage();
        //    message.IsBodyHtml = true;
        //    message.From = new MailAddress(FROM, FROMNAME);
        //    message.To.Add(new MailAddress(TO));
        //    message.Subject = SUBJECT;
        //    message.Body = BODY;
        //    // Comment or delete the next line if you are not using a configuration set
        //    message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);

        //    using (var client = new System.Net.Mail.SmtpClient())
        //    {
        //        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        client.UseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["defaultCredential"]);
        //        client.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSsl"].Trim());
        //        client.Host = HOST;
        //        client.Port = PORT;
        //        client.Credentials = new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);

        //        // Try to send the message. Show status in console.
        //        try
        //        {
        //            Debug.WriteLine("Attempting to send email...");
        //            client.Send(message);
        //            Debug.WriteLine("Email sent!");
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine("The email was not sent.");
        //            Debug.WriteLine("Error message: " + ex.Message);
        //        }
        //    }
        //}
    }
}