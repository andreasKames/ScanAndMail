using MimeKit;
using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Windows;

namespace ScanAndMail
{
    internal class Mail
    {
        internal static void send()
        {
            // neue Nachricht kreieren
            var mail = new MimeMessage
            {
                Sender = new MailboxAddress(ConfManager.GetMyName(), ConfManager.GetMyMailAddress() ),
                Subject = ConfManager.GetSubject(),
               
            };
            mail.To.Add(MailboxAddress.Parse(ConfManager.GetReceiver()));
            mail.Subject = ConfManager.GetSubject();
            var builder = new BodyBuilder();
            builder.TextBody = ConfManager.GetStandardText();
            //mail.Body = new TextPart(MimeKit.Text.TextFormat.Plain) {Text = ConfManager.GetStandardText() };
            //builder.Attachments.Add(ConfManager.GetScannedFile());
            mail.Body = builder.ToMessageBody();
            Console.WriteLine(mail.ToString());
            // Mail senden
            var smtp = new SmtpClient();
            try
            {
                smtp.Connect(ConfManager.GetReceiver(), 587, SecureSocketOptions.StartTls);
                // Nur zu Testzwecken!!
                String login = "mannimustermann2@web.de";
                String password = "asyzVXB7z5eAWtm";
                smtp.Authenticate(login, password);
                //smtp.Authenticate(ConfManager.GetMyMailAddress(), ConfManager.GetHashedPassword().ToString());
                smtp.Send(mail);
                smtp.Disconnect(true);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                MessageBox.Show("Fehler beim Verschicken der E-Mail.\n" + ex.Message);
            }
            
            
        }
    }
}