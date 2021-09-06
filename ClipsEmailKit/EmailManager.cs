using ClipsEmailKit.interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClipsEmailKit
{
    public class EmailManager : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailManager(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;

        }

        public void SendEmail(Message message, string body, bool sslStatus = true)
        {
            var emailMessage = CreateMailMessage(message, body);

            Send(emailMessage, sslStatus);
        }

        public async Task SendEmailAsyn(Message message, string body, bool sslStatus = true)
        {
            var emailMessage = CreateMailMessage(message, body);

            await SendAync(emailMessage, sslStatus);
        }


        private MimeMessage CreateMailMessage(Message message, string body)
        {
            var emailMessage = new MimeMessage();

            string emailBodyContent = body;

            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = emailBodyContent };

            return emailMessage;
        }

        /// <summary>
        /// this is use to connect MimeKit and send email
        /// </summary>
        /// <param name="mailMessage">the message to be send</param>
        /// <param name="sslStatus">ssl status is  set to false by default</param>
        private void Send(MimeMessage mailMessage, bool sslStatus = false)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, sslStatus);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                    client.Send(mailMessage);
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }


        /// <summary>
        /// this is use to connect MimeKit and send email
        /// </summary>
        /// <param name="mailMessage">the message to be send</param>
        /// <param name="sslStatus">ssl status is  set to false by default</param>
        private async Task SendAync(MimeMessage mailMessage, bool sslStatus = false)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, sslStatus);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);

                    await client.SendAsync(mailMessage);
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
