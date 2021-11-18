using ClientMonitor.Application.Abstractions;
using ClientMonitor.Application.Domanes;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMonitor.Infrastructure.Notifications.Adaptors
{
    public class MailAdaptor : INotification
    {
        readonly string Subject = "Уведомление от ClientMonitor";

        readonly string Name = "AFCStudio";

         string Mail = "afc.studio@yandex.ru";

         string Password = "lollipop321123";

        readonly string MailType = "smtp.yandex.ru";

        MimeMessage EmailMessage;

        public MailAdaptor()
        {
            EmailMessage = new MimeMessage();
        }

        IApplicationBuilder application;

        public async Task SendMessage(string to, string massage)
        {
            EmailMessage.From.Add(new MailboxAddress(Name, Mail));
            EmailMessage.To.Add(new MailboxAddress("", to));
            EmailMessage.Subject = Subject;
            EmailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = CreateLatter(Subject, massage)
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                var repository = application.ApplicationServices.GetRequiredService<IRepository<DataForEditInfo>>();
                if (repository.GetData("Mail")!="" && repository.GetData("Pas")!="")
                {
                    Mail = repository.GetData("Mail");
                    Password = repository.GetData("Pas");
                }
                client.Connect(MailType, 587, false);
                client.Authenticate(Mail, Password);
                client.Send(EmailMessage);
                client.Disconnect(true);
            }
        }

        private string CreateLatter(string subject, string content)
        {
            string letter = String.Format("<!DOCTYPE html> <html> <head> <link rel=\"stylesheet\" type=\"text / css\"> </head> <body style=\"margin: 0; padding: 0; \"> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td style=\"background-color:#000000; height: 20px;\"></td></tr></table><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <tr><td style=\"background-color:#ffffff; height: 100%; width: 50%; padding: 20px 10px 0px 0px;\" align=\"right\"> <img src=\"https://afcstudio.ru/spacehome/assets/images/Mlogo.png \" width=\"100px\" height=\"100px\"> </td> <td style=\"background-color:#ffffff; height: 100%; width: 50%; padding: 20px 0px 0px 10px;\"> <p><font size=\"4\" face=\"Quattrocento\">AFCSTUDIO</font></p> <p><font size=\"4\" face=\"Quattrocento\">DEV. STD.</font></p> </td> </tr> </table> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <td style=\"background-color:#ffffff; height: 100%; width: 100%; padding: 20px 0px 20px 0px;\" align=\"center\"> <p><font size=\"3\" face=\"Source Serif Pro\">Студия разработки основана в г. Оренбурге в 2015 году.</font></p> </td> </table><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <td style=\"background-color:#ffffff; height: 100%; width: 100%; padding: 20px 0px 20px 0px;\" align=\"center\"><p><font size=\"4\" face=\"Source Serif Pro\"> {0} </font></p></td></table> <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"> <td style=\"background-color:#ffffff; height: 100%; width: 100%; padding: 20px 40px 20px 40px;\" align=\"left\"> {1} </td> </table> <div style=\" position:absolute; bottom:0; width:100%; background: url(https://afcstudio.ru/spacehome/assets/images/futter.png) no-repeat; background-size: cover;\">  <p style=\"padding: 20px 0px 0px 100px; \"><font size=\"2\" color=\"white\" face=\"Source Serif Pro\">tel. 8 (903) 367-50-03 | *.net, *.ru, *.com</font></p><p style=\"padding: 0px 0px 0px 100px; \"><a size=\"2\" color=\"white\" face=\"Source Serif Pro\" style=\"text-decoration: none; color: white;\">afc.studio@yandex.ru</a></p><p style=\"padding: 0px 0px 40px 100px; \"><a href=\"https://www.afcstudio.ru/\" size=\"2\" color=\"white\" face=\"Source Serif Pro\" style=\"text-decoration: none; color: white;\">www.afcstudio.ru</a></p> </div> </body> </html>", subject, content);
            return letter;
        }
    }
}
