using MimeKit;
using System;
using MailKit.Net.Smtp;
using MailKit.Security;

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
            builder.Attachments.Add(ConfManager.GetScannedFile());
            mail.Body = builder.ToMessageBody();

            // Mail senden
            var smtp = new SmtpClient();
            smtp.Connect(ConfManager.GetReceiver(), 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(ConfManager.GetMyMailAddress(), "[PASSWORD]");
            smtp.Send(mail);
            smtp.Disconnect(true); 
            
        }
    }
}