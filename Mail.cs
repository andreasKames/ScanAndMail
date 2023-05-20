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
            mail.From.Add(MailboxAddress.Parse(ConfManager.GetMyMailAddress() ) );
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
            catch (MailKit.Net.Smtp.SmtpProtocolException ex) 
            {
                MessageBox.Show("Fehler beim Verschicken der E-Mail.\n" + ex.Message);
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Unbekannter Fehler.\n" + ex.Message);
            }
            
        }
        /*
        public static void SendMessage(string receiver, string subject, string standardText)
        {
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(ConfManager.GetMyMailAddress()));
            mail.To.Add(MailboxAddress.Parse(receiver));
            mail.Subject = subject;
            mail.Cc.Add(MailboxAddress.Parse(ConfManager.GetMyMailAddress()));
            var builder = new BodyBuilder();
            builder.TextBody = standardText;
            //mail.Body = new TextPart(MimeKit.Text.TextFormat.Plain) {Text = ConfManager.GetStandardText() };
            builder.Attachments.Add(ConfManager.GetScannedFile());
            mail.Body = builder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(ConfManager.GetMyMailAddress(), 587,SecureSocketOptions.SslOnConnect);
                    //client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                }
                catch (SmtpCommandException ex)
                {
                    Console.WriteLine("Error trying to connect: {0}", ex.Message);
                    Console.WriteLine("\tStatusCode: {0}", ex.StatusCode);
                    return;
                }
                catch (SmtpProtocolException ex)
                {
                    Console.WriteLine("Protocol error while trying to connect: {0}", ex.Message);
                    return;
                }
                catch(System.Net.Sockets.SocketException ex) 
                {
                    Console.WriteLine("SocketException ausgelöst.\nNachricht: {0}\tStatuscode: {1}",ex.Message, ex.ErrorCode);
                    return;
                }

                // Note: Not all SMTP servers support authentication, but GMail does.
                if (client.Capabilities.HasFlag(SmtpCapabilities.Authentication))
                {
                    try
                    {
                        client.Authenticate(ConfManager.GetMyMailAddress(), ConfManager.GetHashedPassword());
                    }
                    catch (AuthenticationException ex)
                    {
                        Console.WriteLine("Invalid user name or password.\nMessage: {0}", ex.Message);
                        return;
                    }
                    catch (SmtpCommandException ex)
                    {
                        Console.WriteLine("Error trying to authenticate: {0}", ex.Message);
                        Console.WriteLine("\tStatusCode: {0}", ex.StatusCode);
                        return;
                    }
                    catch (SmtpProtocolException ex)
                    {
                        Console.WriteLine("Protocol error while trying to authenticate: {0}", ex.Message);
                        return;
                    }
                }

                try
                {
                    client.Send(mail);
                }
                catch (SmtpCommandException ex)
                {
                    Console.WriteLine("Error sending message: {0}", ex.Message);
                    Console.WriteLine("\tStatusCode: {0}", ex.StatusCode);

                    switch (ex.ErrorCode)
                    {
                        case SmtpErrorCode.RecipientNotAccepted:
                            Console.WriteLine("\tRecipient not accepted: {0}", ex.Mailbox);
                            break;
                        case SmtpErrorCode.SenderNotAccepted:
                            Console.WriteLine("\tSender not accepted: {0}", ex.Mailbox);
                            break;
                        case SmtpErrorCode.MessageNotAccepted:
                            Console.WriteLine("\tMessage not accepted.");
                            break;
                    }
                }
                catch (SmtpProtocolException ex)
                {
                    Console.WriteLine("Protocol error while sending message: {0}", ex.Message);
                }
                catch (MailKit.ServiceNotConnectedException ex)
                {
                    Console.WriteLine("ServiceNotConnectedException ausgelöst.\nNachricht: {0}", ex.Message);
                }

                client.Disconnect(true);
            }
        }
        */
    }
}