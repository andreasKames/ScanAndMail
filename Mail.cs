using MimeKit;
using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Windows;

namespace ScanAndMail
{
    internal class Mail
    {
        internal static void Send(string receiver, string subject, string standardText)
        {
            // neue Nachricht kreieren
            var mail = new MimeMessage();
            mail.From.Add( MailboxAddress.Parse(ConfManager.GetMyMailAddress() ) );
           
            //mail.Sender.Name = ConfManager.GetMyName();
            mail.To.Add(MailboxAddress.Parse(receiver ));
            mail.Subject = subject;
            //mail.Cc.Add(MailboxAddress.Parse(ConfManager.GetMyMailAddress() ) );
            var builder = new BodyBuilder();
            builder.TextBody = standardText;
            builder.Attachments.Add(ConfManager.GetScannedFile());
            mail.Body = builder.ToMessageBody();
            var smtp = new SmtpClient();
            

            try
            {
                smtp.Connect(ConfManager.GetSMTP_Server(), 587, SecureSocketOptions.StartTls);
               // Console.WriteLine("\nPasswort: " + ConfManager.GetHashedPassword() +"\n");
                smtp.Authenticate(ConfManager.GetMyMailAddress(), ConfManager.GetHashedPassword() );
                smtp.Send(mail);
                smtp.Disconnect(true);
                MessageBox.Show("E-Mail erfolgreich gesendet.");
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                MessageBox.Show("Fehler beim Verschicken der E-Mail.\n" + ex.Message);
            }
            catch (SmtpProtocolException ex) 
            {
                MessageBox.Show("Fehler beim Verschicken der E-Mail.\n" + ex.Message);
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Unbekannter Fehler.\n" + ex.Message);
            }            
        }       
    }
}